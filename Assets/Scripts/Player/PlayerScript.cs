using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private bool inDialog;


    //HPSystem
    public int HP = 8;
    public GameObject[] FullHearts;
    public GameObject[] HalfHearts;
    public GameObject[] EmptyHearts;

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
    private bool attackAnimation1;
    private bool attackAnimation2;
    private bool attackAnimation3;
    private bool attackAnimation4;
    private bool deathAnimation;
    private bool hitAnimation;
    private bool rollAnimation;

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



    //FOR DASH - NOT FINISHED
    public bool dash1Ready;
    public bool dash2Ready;
    public bool dash3Ready;
    public float dashSpeed = 10f;
    public float dashTime = .1f;







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
        Interact();


        /*
        StartCoroutine("Dash1");
        StartCoroutine("Dash2");
        StartCoroutine("Dash3");
        //TESTKODE - FJERN SENERE
        if (Input.GetKeyDown(KeyCode.F)) {
            animator.SetBool(isHitHash, true);
        }
        if (Input.GetKeyDown(KeyCode.G)) {
            animator.SetBool(isDeadHash, true);
        }
        else {
            animator.SetBool(isHitHash, false);
            animator.SetBool(isDeadHash, false);
        }*/
    }


    //for initialization 
    private void initiate() {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        cam = GameObject.Find("Main Camera").GetComponent<Transform>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        InteractText = GameObject.Find("InteractText");

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

        Cursor.lockState = CursorLockMode.Locked;
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
        deathAnimation = animator.GetCurrentAnimatorStateInfo(0).IsTag("Death");
        hitAnimation = animator.GetCurrentAnimatorStateInfo(0).IsTag("Hit");
        attackAnimation1 = animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack1");
        attackAnimation2 = animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack2");
        attackAnimation3 = animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack3");
        attackAnimation4 = animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack4");
        rollAnimation = animator.GetCurrentAnimatorStateInfo(0).IsTag("Rolling");
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
            if (Input.GetButtonDown("Jump") && isGrounded && !rollAnimation) {
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

    private void animatePlayer() {
        if (inDialog) {
            animator.SetBool(isWalkingHash, false);
            animator.SetBool(isRunningHash, false);
            animator.SetBool(isCrouchingHash, false);
        }
        if (!inDialog) {
            //checks if idle / runnig / walking
            if (idleAnimation) {
                dash1Ready = true;
                dash2Ready = true;
                dash3Ready = true;
            }

            //walking animation
            if ((forwardPressed || backwardPressed || leftPressed || rightPressed)) {
                animator.SetBool(isWalkingHash, true);
            } else {
                animator.SetBool(isWalkingHash, false);
            }

            //running animation
            if ((forwardPressed || backwardPressed || leftPressed || rightPressed) && runPressed) {
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
            if (attackPressed && !attackAnimation1 && !attackAnimation2 && !attackAnimation3) {
                animator.SetBool(isAttack1Hash, true);
                crouchingToggle = false;
            }


            //attack 2
            if (attackPressed && attackAnimation1) {
                animator.SetBool(isAttack2Hash, true);
            }



            //attack 3
            if (attackPressed && attackAnimation2) {
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
            if (attackAnimation4) {
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
            if (rollPressed) {
                animator.SetBool(isRollingHash, true);
                crouchingToggle = false;
            }
            else {
                animator.SetBool(isRollingHash, false);
            }
        }
        

    }
    /*IEnumerator Dash1() {
        bool attack1playing = animator.GetCurrentAnimatorStateInfo(0).IsTag("1");
        yield return new WaitUntil(() => attack1playing);

        yield return new WaitForSeconds(0.20f);

        float startTime = Time.time; // need to remember this to know how long to dash
        while (Time.time < startTime + dashTime) {
            controller.Move(transform.forward * dashSpeed * Time.deltaTime);
            yield return null; // this will make Unity stop here and continue next frame
            }
        
        
    }
    IEnumerator Dash2() {
        bool attack2playing = animator.GetCurrentAnimatorStateInfo(0).IsTag("2");
        yield return new WaitUntil(() => attack2playing);
        yield return new WaitForSeconds(0.50f);
        float startTime = Time.deltaTime; // need to remember this to know how long to dash
        while (Time.time < startTime + dashTime) {
            controller.Move(transform.forward * dashSpeed * Time.deltaTime);
            yield return null; // this will make Unity stop here and continue next frame
        }
    }
    IEnumerator Dash3() {
        bool attack3playing = animator.GetCurrentAnimatorStateInfo(0).IsTag("3");
        yield return new WaitUntil(() => attack3playing);

        yield return new WaitForSeconds(0.50f);
        float startTime = Time.time; // need to remember this to know how long to dash
        while (Time.time < startTime + dashTime) {
            controller.Move(transform.forward * dashSpeed * Time.deltaTime);
            yield return null; // this will make Unity stop here and continue next frame
        }
    }*/
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
    private void Interact() {
        ClosestTarget = GetClosestEnemy().GetComponent<Interaction>();

        if (DistanceToClosestTarget() <= 1.5f && !ClosestTarget.getStartInteraction()) {
            InteractText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E)) {
                ClosestTarget.setStartInteraction(true);
            }
            
        } else {
            InteractText.SetActive(false);
        }
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

        foreach (GameObject potentialTarget in gm.ListOfInteractables) {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr) {
                closestDistanceSqr = dSqrToTarget;
                ClosestTarget = potentialTarget;
            }
        }
        return ClosestTarget;
    }


    public void lookAtTarget(Transform target) {
        Vector3 lookPos = target.position - transform.position;
        lookPos.y = 0;
        Quaternion rotate = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotate, Time.deltaTime * 2);
    }





    //getters and setters
    public Animator getAnimator() {
        return animator;
    }

    public bool getInDialog() {
        return inDialog;
    }
    public void setInDialog(bool inDialog) {
        this.inDialog = inDialog;
    }
}





