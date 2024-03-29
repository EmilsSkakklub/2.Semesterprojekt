using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
    public string enemyName;
    public bool isDead;
    public float removeTimer;

    public PlayerScript ps;

    private Animator animator;
    private Transform playerTransform;
    private Transform cameraTransform;
    private CharacterController controller;

    protected AudioManager audioManager;

    private Vector3 velocity;
    private float gravity = -9.81f;
    public bool isGrounded;

    private float moveSpeed;
    private float MaxDistance = 2f;
    private float MinDistance = 1f;

    //indicate what animation is playing
    public bool walkAnimation;
    private bool deadAnimation;
    private bool attackAnimation;
    private bool hitAnimation;

    //hash codes for the animations
    private int isWalkingHash;
    private int isDeadHash;
    private int isAttackingHash;
    private int isHitHash;
    private int walkSpeedHash;

    //combat
    public Transform attackpoint;
    public int attackDamage;
    private float attackRange = 0.8f;
    public LayerMask playerLayers;
    public bool hit = true;
    public bool isHit;
    private float attackTimer = 0;
    private float attackStart = 0.5f;
    private float attackEnd = 1f;

    //health
    public int health;
    public int maxHealth;
    public Slider healthSlider;

    //vision cone
    public float visionRange;
    public float newVisionRange;
    public float visionConeAngle = 30;

    private float surroundVisionRange = 2f;
    private float surroundVisionConeAngle = 360;
    public bool hasDetectedPlayer;

    public string theHitSound;

    protected void initStart(string enemyName, int attackDamage, int maxHealth, float moveSpeed, float attackRange, float visionRange, string theHitSound) {

        gameObject.tag = "Enemy";
        gameObject.layer = LayerMask.NameToLayer("Enemy");
        playerLayers = LayerMask.GetMask("Player");

        setEnemyName(enemyName);
        setAttackDamage(attackDamage);
        setMaxHealth(maxHealth);
        setHealth(maxHealth);
        setMoveSpeed(moveSpeed);
        setAttackRange(attackRange);
        setVisionRange(visionRange);
        setRemoveTimer(3);
        setTheHitSound(theHitSound);

        ps = GameObject.Find("Player").GetComponent<PlayerScript>();
        animator = GetComponent<Animator>();
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        cameraTransform = GameObject.Find("Main Camera").GetComponent<Transform>();
        controller = GetComponent<CharacterController>();
        audioManager = GameObject.Find("GameManager").GetComponent<AudioManager>();

        setAnimationHashCodes();

    }

    protected void initUpdate() {

        getCurrentAnimationPlaying();
        updateGravity();
        groundCheck();
        goTowardsEnemy();
        updateAnimations();
        setHealthSlider(health);
        attack();
        die();
        updateVisionCone();
        removeCanvas();

    }

    private void setAnimationHashCodes() {
        isWalkingHash = Animator.StringToHash("isWalking");
        isDeadHash = Animator.StringToHash("isDead");
        isAttackingHash = Animator.StringToHash("isAttacking");
        isHitHash = Animator.StringToHash("isHit");
        walkSpeedHash = Animator.StringToHash("walkSpeed");
    }

    private void removeCanvas() {
        if (health <= 0 || Vector3.Distance(transform.position, ps.transform.position) > 10) {
            GetComponentInChildren<Canvas>().enabled = false;
        } else {
            GetComponentInChildren<Canvas>().enabled = true;
        }
    }
    private void getCurrentAnimationPlaying() {
        walkAnimation = animator.GetCurrentAnimatorStateInfo(0).IsTag("Walk");
        deadAnimation = animator.GetCurrentAnimatorStateInfo(0).IsTag("Dead");
        attackAnimation = animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack");
        hitAnimation = animator.GetCurrentAnimatorStateInfo(0).IsTag("Hit");

        //set the speed of the walk animation to be equal to moveSpeed 
        animator.SetFloat(walkSpeedHash, moveSpeed);
    }

    private void updateGravity() {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }
    }

    private void groundCheck() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.07f + 0.02f)) {
            isGrounded = true;
        }
        else {
            isGrounded = false;
        }
    }

    private void updateAnimations() {
        //the y rotation of the enemy should be static
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        //avoid replaying the hit animation on repeat
        if (hitAnimation) {
            animator.SetBool(isHitHash, false);
        }
    }





    public bool hitSound;

    public void takeDamage(int damage) {
        if (!hitSound) {
            audioManager.Play(theHitSound, false, 0.05f, Random.Range(0.8f, 2));
            hitSound = true;
        }
        hitSound = false;

        if (!isDead) {
            setDetectedPlayer(true);
            animator.SetBool(isHitHash, true);
            health -= damage;
        }
    }

    public void ResetIsHit() {
        isHit = false;
    }

    private void die() {
        if(health <= 0) {
            setIsDead(true);
            animator.SetBool(isDeadHash, true);
            if (deadAnimation) {
                animator.SetBool(isDeadHash, false);
                removeTimer -= Time.deltaTime;
            }
            if(removeTimer <= 0) {
                Destroy(gameObject);
            }
        }
    }

    private void goTowardsEnemy() {
        if (hasDetectedPlayer && !isDead) {
            Vector3 lookPos = playerTransform.position - transform.position;
            lookPos.y = 0;
            Quaternion rotate = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotate, Time.deltaTime * 5);
        }

        if (!isDead && !hitAnimation && !attackAnimation && hasDetectedPlayer) {
            if (Vector3.Distance(transform.position, playerTransform.position) >= MinDistance) {
                animator.SetBool(isWalkingHash, true);
                transform.position += transform.forward * moveSpeed * 2 * Time.deltaTime;

                if (Vector3.Distance(transform.position, playerTransform.position) <= MaxDistance) {
                    animator.SetBool(isAttackingHash, true);
                }
                else {
                    animator.SetBool(isAttackingHash, false);
                }
            }
            else {
                animator.SetBool(isWalkingHash, false);
            }
        }
        else if (!hasDetectedPlayer) {
            animator.SetBool(isWalkingHash, false);
            animator.SetBool(isAttackingHash, false);
        }
    }


    private void attack() {
        Collider[] hitPlayer = Physics.OverlapSphere(attackpoint.position, attackRange, playerLayers);
        if (attackAnimation) {
            attackTimer += Time.deltaTime;
        }

        if (attackAnimation && hit && attackTimer > attackStart && attackTimer < attackEnd) {
            foreach (Collider player in hitPlayer) {
                if (!ps.invincibility) {
                    player.GetComponent<PlayerScript>().takeDamage(attackDamage);
                }           
                hit = false;
            }
        }
        if (!attackAnimation) {
            hit = true;
            attackTimer = 0f;
        }
    }


    private void OnDrawGizmosSelected() {
        if (attackpoint == null) {
            return;
        }
        Gizmos.DrawWireSphere(attackpoint.position, attackRange);
    }




    //vision cone method
    public void updateVisionCone() {
        Vector3 vectorToPlayer = playerTransform.position - transform.position;
        if (hasDetectedPlayer) {
            newVisionRange = visionRange * 2;
            surroundVisionRange = 4;
        }

        //surrounding vision
        if (Vector3.Distance(transform.position, playerTransform.position) <= surroundVisionRange && !ps.getCrouchToggle()) {
            if (Vector3.Angle(transform.forward, vectorToPlayer) <= surroundVisionConeAngle) {
                setDetectedPlayer(true);
            }
            else {
                setDetectedPlayer(false);
            }
        }
        //forward vision
        else if (Vector3.Distance(transform.position, playerTransform.position) <= newVisionRange && !ps.getIsStealth()) {
            if (Vector3.Angle(transform.forward, vectorToPlayer) <= visionConeAngle) {
                setDetectedPlayer(true);
            }
            else {
                setDetectedPlayer(false);
            }
        }
        else {
            setDetectedPlayer(false);
            newVisionRange = visionRange;
            surroundVisionRange = 2;
        }
    }


    public void setHealthSlider(int health) {
        healthSlider.value = health;
        healthSlider.transform.rotation = cameraTransform.transform.rotation;
    }












    //getters and setters
    public int getHealth() {
        return health;
    }

    public void setHealth(int health) {
        this.health = health;
    }

    public int getMaxHealth() {
        return maxHealth;
    }

    public void setMaxHealth(int maxHealth) {
        this.maxHealth = maxHealth;
    }

    public void setMoveSpeed(float moveSpeed) {
        this.moveSpeed = moveSpeed;
    }

    public void setAttackDamage(int attackDamage) {
        this.attackDamage = attackDamage;
    }

    public void setAttackRange(float attackRange) {
        this.attackRange = attackRange;
    }


    public string getEnemyName() {
        return enemyName;
    }

    public void setEnemyName(string enemyName) {
        this.enemyName = enemyName;
    }


    public bool getIsDead() {
        return isDead;
    }
    public void setIsDead(bool isDead) {
        this.isDead = isDead;
    }

    public void setRemoveTimer(float removeTimer) {
        this.removeTimer = removeTimer;
    }


    public bool getDetectedPlayer() {
        return hasDetectedPlayer;
    }

    public void setDetectedPlayer(bool isDetected) {
        this.hasDetectedPlayer = isDetected;
    }

    public void setVisionRange(float visionRange) {
        this.visionRange = visionRange;
    }

    public void setTheHitSound(string theHitSound) {
        this.theHitSound = theHitSound;
    }

    
}
