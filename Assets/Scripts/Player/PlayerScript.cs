using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    //major components
    private CharacterController controller;
    private Animator animator;
    private Transform cam;
    private GameManager gm;
    public LayerMask groundMask;


    //movement
    Vector3 velocity;
    public bool isGrounded;
    public bool jumpNotReady;
    private float jumpHeight = .75f;
    private float gravity = -9.81f;
    private float speed = 2f;
    private float SmoothTurn = 0.1f;
    private float TurnSmoothVelocity;
    private bool crouchingToggle = false;

    //interact
    Interaction ClosestTarget;
    GameObject InteractText;
    GameObject textBubble;
    public List<GameObject> ListOfInteractables;
    private bool inDialog;

    //HP System
    public int HP = 8;
    public GameObject[] FullHearts;
    public GameObject[] HalfHearts;
    public GameObject[] EmptyHearts;

    //stamina System
    public float Stamina;
    public float maxStamina;
    public float recoveryTimer = 1f;
    public bool animationHasStarted = false;
    public bool animationAttStarted1 = false;
    public bool animationAttStarted2 = false;
    public bool animationAttStarted3 = false;
    public Slider staminaSlider;

    //combat system
    public int attackDamage;
    public Transform attackpoint;
    public float attackRange = 0.3f;
    public LayerMask enemyLayers;
    public bool hit1 = true;
    public bool hit2 = true;
    public bool hit3 = true;
    public bool hit4 = true;

    //input booleans
    private bool forwardPressed;
    private bool runPressed;
    private bool backwardPressed;
    private bool leftPressed;
    private bool rightPressed;
    private bool jumpPressed;
    private bool attackPressed;
    private bool crouchPressed;
    private bool rollPressed;

    //bools to see what animation is currently playing
    private bool idleAnimation;
    private bool walkAnimation;
    private bool runningAnimation;
    public bool attackAnimation1;
    public bool attackAnimation2;
    public bool attackAnimation3;
    public bool attackAnimation4;
    private bool deathAnimation;
    private bool hitAnimation;
    public bool rollAnimation;
    private bool jumpAnimation;


    //hash codes for  the animator 
    private int isWalkingHash;
    private int isRunningHash;
    private int isJumpingHash;
    private int isAttack1Hash;
    private int isAttack2Hash;
    private int isAttack3Hash;
    private int isCrouchingHash;
    private int isHitHash;
    private int isDeadHash;
    private int isRollingHash;

    
    private void Start() {
        initiate();
    }
    private void FixedUpdate() {
        groundCheck();
    }
    void Update()
    {
        movement();
        checkCurrentAnimationPlaying();
        animatePlayer();
        inputManager();
        
        updateHealth();
        
        updateStamina();
        recoverStamina(3);
        setStaminaSlider(Stamina);
        StartCoroutine(Interact());

        attack();
    
    }


    //for initialization 
    private void initiate() {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        cam = GameObject.Find("Main Camera").GetComponent<Transform>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        InteractText = GameObject.Find("InteractText");
        textBubble = GameObject.Find("TextBubble");
        staminaSlider = GameObject.Find("StaminaBar").GetComponent<Slider>();
        attackpoint = GameObject.Find("AttackPoint").GetComponent<Transform>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isJumpingHash = Animator.StringToHash("isJumping");
        isAttack1Hash = Animator.StringToHash("isAttack1");
        isAttack2Hash = Animator.StringToHash("isAttack2");
        isAttack3Hash = Animator.StringToHash("isAttack3");
        isHitHash = Animator.StringToHash("isHit");
        isDeadHash = Animator.StringToHash("isDead");
        isCrouchingHash = Animator.StringToHash("isCrouching");
        isRollingHash = Animator.StringToHash("isRolling");

        gameObject.layer = LayerMask.NameToLayer("Player");

        Cursor.lockState = CursorLockMode.Locked;

        setAttackDamage(1);
        setMaxStamina(1000);
        setStamina(maxStamina);

        foreach (GameObject interactable in GameObject.FindGameObjectsWithTag("Interactable")) {
            ListOfInteractables.Add(interactable);
        }
    }

    //checks the current input of the player
    private void inputManager() {
        forwardPressed = Input.GetKey(KeyCode.W);
        runPressed = Input.GetKey(KeyCode.LeftShift);
        backwardPressed = Input.GetKey(KeyCode.S);
        leftPressed = Input.GetKey(KeyCode.A);
        rightPressed = Input.GetKey(KeyCode.D);
        jumpPressed = Input.GetKeyDown(KeyCode.Space);
        attackPressed = Input.GetMouseButtonDown(0);
        crouchPressed = Input.GetKeyDown(KeyCode.C);
        rollPressed = Input.GetMouseButtonDown(1);
    }

    //checks which animation is running 
    private void checkCurrentAnimationPlaying() {
        idleAnimation = animator.GetCurrentAnimatorStateInfo(0).IsTag("Idle");
        walkAnimation = animator.GetCurrentAnimatorStateInfo(0).IsTag("Walk");
        runningAnimation = animator.GetCurrentAnimatorStateInfo(0).IsTag("Run");
        deathAnimation = animator.GetCurrentAnimatorStateInfo(0).IsTag("Death");
        hitAnimation = animator.GetCurrentAnimatorStateInfo(0).IsTag("Hit");
        attackAnimation1 = animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack1");
        attackAnimation2 = animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack2");
        attackAnimation3 = animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack3");
        attackAnimation4 = animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack4");
        rollAnimation = animator.GetCurrentAnimatorStateInfo(0).IsTag("Rolling");
        jumpAnimation = animator.GetCurrentAnimatorStateInfo(0).IsTag("Jump");
    }

//ability for player to move around in the world
private void movement() {
        //Gets Axis from Input Manager
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        //prevents from using y-axis (Normalized so you wont move faster when pressing W && D/A)
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        //player can't move while hit or death animation is playing
        if (!hitAnimation && !deathAnimation && !inDialog) {
            //move
            if (direction.magnitude >= 0.1f) {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref TurnSmoothVelocity, SmoothTurn);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                //mopve a little faster while rolling
                if (rollAnimation) {
                    controller.Move(moveDir.normalized * speed* 1.5f * Time.deltaTime);
                }
                else {
                    controller.Move(moveDir.normalized * speed * Time.deltaTime);
                }
            }

            // jump
            if (Input.GetButtonDown("Jump") && isGrounded && !rollAnimation && getStamina() > 0) {
                StartCoroutine("Jump");
            }
        }

        //gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if(isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }
    }

    //method for checking if the player is grounded
    void groundCheck() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.07f + 0.02f)) {
            isGrounded = true;
        } else {
            isGrounded = false;
        }
    }

    //jump method
    IEnumerator Jump() {
        animator.SetBool(isJumpingHash, true);
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        yield return new WaitForSeconds(0.2f);
        animator.SetBool(isJumpingHash, false);
        crouchingToggle = false;
    }

    private void animatePlayer(){
        if (inDialog) {
            animator.SetBool(isWalkingHash, false);
            animator.SetBool(isRunningHash, false);
            animator.SetBool(isCrouchingHash, false);
        }
        if (!inDialog) {
            //checks if idle / runnig / walking
            if (idleAnimation) {


            }

            //walking animation
            if ((forwardPressed || backwardPressed || leftPressed || rightPressed)) {
                animator.SetBool(isWalkingHash, true);
            } else {
                animator.SetBool(isWalkingHash, false);
            }

            //running animation
            if ((forwardPressed || backwardPressed || leftPressed || rightPressed) && runPressed && getStamina() > 0) {
                animator.SetBool(isRunningHash, true);
                crouchingToggle = false;
                speed = 4;
            } else {
                animator.SetBool(isRunningHash, false);
                speed = 2;
            }

            //crouching animation
            if (crouchPressed) {
                crouchingToggle = !crouchingToggle;
            }

            if (crouchingToggle) {
                animator.SetBool(isCrouchingHash, true);

                //Corrects hitbox and speed
                controller.height = 0.1f;
                controller.center = new Vector3(0f, 0.05f, 0f);
                speed = 1;
            } else if (!crouchingToggle) {
                animator.SetBool(isCrouchingHash, false);

                //Corrects hitbox and speed
                controller.height = 0.14f;
                controller.center = new Vector3(0f, 0.075f, 0f);
            }


            //attack animation
            //attack 1
            if (attackPressed && !attackAnimation1 && !attackAnimation2 && !attackAnimation3 && Stamina > 0) {
                animator.SetBool(isAttack1Hash, true);
                crouchingToggle = false;
            }


            //attack 2
            if (attackPressed && attackAnimation1 && Stamina > 0) {
                animator.SetBool(isAttack2Hash, true);
            }



            //attack 3
            if (attackPressed && attackAnimation2 && Stamina > 0) {
                animator.SetBool(isAttack3Hash, true);
            }


            if (!attackPressed && attackAnimation1) {
                animator.SetBool(isAttack1Hash, false);

            }
            if (!attackPressed && attackAnimation2) {
                animator.SetBool(isAttack2Hash, false);

            }
            if (!attackPressed && attackAnimation3) {
                animator.SetBool(isAttack3Hash, false);

            }

            //jump attack animation
            if (attackAnimation4 && Stamina > 0) {
                animator.SetBool(isAttack1Hash, false);
            }

            //die animation
            if (HP <= 0 && !hitAnimation) {
                animator.SetBool(isDeadHash, true);
                if (deathAnimation) {
                    animator.SetBool(isDeadHash, false);
                }
            }

            //roll animation
            if (rollPressed && Stamina > 0 && !rollAnimation) {
                animator.SetBool(isRollingHash, true);
                crouchingToggle = false;
            }
            else {
                animator.SetBool(isRollingHash, false);
            }

            //hit animation
            if (hitAnimation) {
                animator.SetBool(isHitHash, false);
            }
        }
    }

    private void attack() {
        Collider[] hitEnemies = Physics.OverlapSphere(attackpoint.position, attackRange, enemyLayers);
        if (attackAnimation1 && hit1) {
            foreach (Collider enemy in hitEnemies) {
                enemy.GetComponent<Enemy>().takeDamage(attackDamage);
                hit1 = false;
            }
        }
        if (attackAnimation2 && hit2) {
            foreach (Collider enemy in hitEnemies) {
                enemy.GetComponent<Enemy>().takeDamage(attackDamage);
                hit2 = false;
            }
        }
        if (attackAnimation3 && hit3) {
            foreach (Collider enemy in hitEnemies) {
                enemy.GetComponent<Enemy>().takeDamage(attackDamage);
                hit3 = false;
            }
        }
        if (attackAnimation4 && hit4) {
            foreach (Collider enemy in hitEnemies) {
                enemy.GetComponent<Enemy>().takeDamage(attackDamage);
                hit4 = false;
            }
        }
        if(!attackAnimation1 && !attackAnimation2 && !attackAnimation3 && !attackAnimation4) {
            hit1 = true;
            hit2 = true;
            hit3 = true;
            hit4 = true;
        }
    }

    private void OnDrawGizmosSelected() {
        if(attackpoint == null) {
            return;
        }
        Gizmos.DrawWireSphere(attackpoint.position, attackRange);
    }

    public void takeDamage(int damage) {
        if (!rollAnimation) {
            if (!deathAnimation) {
                animator.SetBool(isHitHash, true);
            }
            if (HP > 0) {
                HP -= damage;
            }
        }
    }


    private void updateHealth() {
        //First [i] = 4. i.e. the array goes from 4 - 3 - 2 - 1 - 0.
        
        if (Input.GetKeyDown(KeyCode.R) && HP > 0) {
            animator.SetBool(isHitHash, true);
            HP--;
        }
        else {
            animator.SetBool(isHitHash, false);
        }

        if (Input.GetKeyDown(KeyCode.T) && HP < 8) {
            HP++;
        }

        
        //First
        if (HP == 7) {
            FullHearts[3].gameObject.SetActive(false);
        }
        if (HP > 7) {
            FullHearts[3].gameObject.SetActive(true);
        }
        if (HP > 6) {
            HalfHearts[3].gameObject.SetActive(true);
        }
        else if (HP == 6) {
            HalfHearts[3].gameObject.SetActive(false);
        }


        //Second
        else if (HP == 5) {
            FullHearts[2].gameObject.SetActive(false);
        }
        if (HP > 5) {
            FullHearts[2].gameObject.SetActive(true);
        }
        if (HP > 4) {
            HalfHearts[2].gameObject.SetActive(true);
        }

        else if (HP == 4) {
            HalfHearts[2].gameObject.SetActive(false);
        }



        //Third
        else if (HP == 3) {
            FullHearts[1].gameObject.SetActive(false);
        }
        if (HP > 3) {
            FullHearts[1].gameObject.SetActive(true);
        }
        if (HP > 2) {
            HalfHearts[1].gameObject.SetActive(true);
        }
        else if (HP == 2) {
            HalfHearts[1].gameObject.SetActive(false);
        }


        //fourth and Last
        else if (HP == 1) {
            FullHearts[0].gameObject.SetActive(false);
        }
        if (HP > 1) {
            FullHearts[0].gameObject.SetActive(true);
        }
        if (HP > 0) {
            HalfHearts[0].gameObject.SetActive(true);
        }
        else if (HP == 0) {
            HalfHearts[0].gameObject.SetActive(false);
        }
    }

    //interaction
    private IEnumerator Interact() {
        if(ListOfInteractables.Count != 0) {
            ClosestTarget = GetClosestEnemy().GetComponent<Interaction>();

            if (DistanceToClosestTarget() <= 1.5f && !ClosestTarget.getStartInteraction()) {
                InteractText.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E)) {
                    yield return new WaitForSeconds(0.01f);
                    ClosestTarget.setStartInteraction(true);
                }
            } else {
                InteractText.SetActive(false);
                textBubble.SetActive(false);
            }
        }
        else {
            InteractText.SetActive(false);
            textBubble.SetActive(false);
        }
        
        yield return null;
    }
    //finds the distance to the closest interactable target
    public float DistanceToClosestTarget() {
        float dist;
        dist = Vector3.Distance(GetClosestEnemy().transform.position, transform.position);

        return dist;
    }
    //finds the closest interactable target
  
    
    
    
    public GameObject GetClosestEnemy() {
        GameObject ClosestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject potentialTarget in ListOfInteractables) {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr) {
                closestDistanceSqr = dSqrToTarget;
                ClosestTarget = potentialTarget;
            }
        }
        return ClosestTarget;
    }
    //look at target
    public void lookAtTarget(Transform target) {
        Vector3 lookPos = target.position - transform.position;
        lookPos.y = 0;
        Quaternion rotate = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotate, Time.deltaTime * 2);
    }


    public void loseStaminaPeriodically() {
        if(Stamina > 0) {
            Stamina -= 2 * Time.deltaTime;
        }
        if(Stamina < 0) {
            setStamina(0);
        }
       
    }

    public void loseStaminaInstantly(int staminaCost) {
        float staminaLeft = Stamina - staminaCost;
        if(staminaLeft <= 0) {
            setStamina(0);
        }
        else if(staminaLeft > 0){
            Stamina -= staminaCost;
        }
    }

    //recover stamina passively when not doing any major action
    private void recoverStamina(float staminaRecoveryRate) {
        if (runningAnimation || rollAnimation || jumpAnimation || attackAnimation1 || attackAnimation2 || attackAnimation3 || attackAnimation4) {
            recoveryTimer = 1f;
        }
        if(recoveryTimer < 0) {
            recoveryTimer = 0;
        }
        if(!runningAnimation && Stamina < maxStamina) {
            recoveryTimer -= Time.deltaTime;
            if(recoveryTimer <= 0) {
                Stamina += staminaRecoveryRate * Time.deltaTime;
            }
        }
        if(Stamina > maxStamina) {
            Stamina = maxStamina;
        } 
    }

    
    private void updateStamina() {
        if(walkAnimation || idleAnimation) {
            animationHasStarted = false;
            animationAttStarted1 = false;
            animationAttStarted2 = false;
            animationAttStarted3 = false;
        }

        if (runningAnimation) {
            loseStaminaPeriodically();
        }
        if (jumpAnimation && !animationHasStarted) {
            animationHasStarted = true;
            loseStaminaInstantly(1);
        }
        if (attackAnimation1 && !animationAttStarted1) {
            animationAttStarted1 = true;
            loseStaminaInstantly(1);
        }
        if (attackAnimation2 && !animationAttStarted2) {
            animationAttStarted2 = true;
            loseStaminaInstantly(1);
        }
        if (attackAnimation3 && !animationAttStarted3) {
            animationAttStarted3 = true;
            loseStaminaInstantly(2);
        }
        if (attackAnimation4 && !animationAttStarted1) {
            animationAttStarted1 = true;
            loseStaminaInstantly(1);
        }
        if (rollAnimation && !animationHasStarted) {
            animationHasStarted = true;
            loseStaminaInstantly(1);
        }

    }

    public void setStaminaSlider(float Stamina) {
        staminaSlider.value = Stamina;
    }

    public void setMaxStaminaSlider(float maxStamina) {
        staminaSlider.maxValue = maxStamina;
        staminaSlider.value = maxStamina;
    }





    //getters and setters
    public Animator getAnimator() {
        return animator;
    }


    public float getStamina() {
        return Stamina;
    }
    public void setStamina(float Stamina) {
        this.Stamina = Stamina;
    }

    public float getMaxStamina() {
        return maxStamina;
    }
    public void setMaxStamina(float maxStamina) {
        this.maxStamina = maxStamina;
    }

    public float getattackDamage() {
        return attackDamage;
    }
    public void setAttackDamage(int attackDamage) {
        this.attackDamage = attackDamage;
    }

    public bool getInDialog() {
        return inDialog;
    }
    public void setInDialog(bool inDialog) {
        this.inDialog = inDialog;
    }

}





