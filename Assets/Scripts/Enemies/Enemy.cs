using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
    
    public string enemyName;
    public bool isDead;
    public float removeTimer;

    public PlayerScript player;

    private Animator animator;
    private Transform playerTransform;
    private Transform cameraTransform;
    private CharacterController controller;
    

    private Vector3 velocity;
    private float gravity = -9.81f;
    public bool isGrounded;

    public float moveSpeed;
    private float MaxDistance = 2f;
    private float MinDistance = 1f;

    //indicate what animation is playing
    private bool walkAnimation;
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
    private float attackTimer = 0;
    private float attackStart = 0.5f;
    private float attackEnd = 1f;

    //health
    public int health;
    public int maxHealth;
    public Slider healthSlider;

    //vision cone
    private float visionRange = 5;
    private float visionConeAngle = 30;

    private float surroundVisionRange = 2f;
    private float surroundVisionConeAngle = 360;
    public bool isDetected;



    protected void initStart(string enemyName, int attackDamage, int maxHealth, float moveSpeed) {

        gameObject.layer = LayerMask.NameToLayer("Enemy");
        playerLayers = LayerMask.GetMask("Player");

        setEnemyName(enemyName);
        setAttackDamage(attackDamage);
        setMaxHealth(maxHealth);
        setHealth(maxHealth);
        setMoveSpeed(moveSpeed);
        setRemoveTimer(3);

        player = GameObject.Find("Player").GetComponent<PlayerScript>();
        animator = GetComponent<Animator>();
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        cameraTransform = GameObject.Find("Main Camera").GetComponent<Transform>();
        controller = GetComponent<CharacterController>();

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
    }

  
    
    private void setAnimationHashCodes() {
        isWalkingHash = Animator.StringToHash("isWalking");
        isDeadHash = Animator.StringToHash("isDead");
        isAttackingHash = Animator.StringToHash("isAttacking");
        isHitHash = Animator.StringToHash("isHit");
        walkSpeedHash = Animator.StringToHash("walkSpeed");
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

    public void takeDamage(int damage) {
        if (!isDead) {
            setIsDetected(true);
            animator.SetBool(isHitHash, true);
            health -= damage;
        }
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
        if (isDetected && !isDead) {
            Vector3 lookPos = playerTransform.position - transform.position;
            lookPos.y = 0;
            Quaternion rotate = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotate, Time.deltaTime * 5);
        }

        if (!isDead && !hitAnimation && !attackAnimation && isDetected) {
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
        else if (!isDetected) {
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
                player.GetComponent<PlayerScript>().takeDamage(attackDamage);
                Debug.Log(player.name + " hit");
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


        if (isDetected) {
            visionRange = 10;
            surroundVisionRange = 4;
        }

        //surrounding vision
        if (Vector3.Distance(transform.position, playerTransform.position) <= surroundVisionRange && !player.getCrouchToggle()) {
            if (Vector3.Angle(transform.forward, vectorToPlayer) <= surroundVisionConeAngle) {
                setIsDetected(true);
                
            }
            else {
                setIsDetected(false);
            }
        }
        //forward vision
        else if (Vector3.Distance(transform.position, playerTransform.position) <= visionRange) {
            if (Vector3.Angle(transform.forward, vectorToPlayer) <= visionConeAngle) {
                setIsDetected(true);
            }
            else {
                setIsDetected(false);
            }
        }
        else {
            setIsDetected(false);
            visionRange = 5;
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



    public string getEnemyName() {
        return enemyName;
    }

    public void setEnemyName(string enemyName) {
        this.enemyName = enemyName;
    }

    public void setIsDead(bool isDead) {
        this.isDead = isDead;
    }

    public void setRemoveTimer(float removeTimer) {
        this.removeTimer = removeTimer;
    }


    public void setIsDetected(bool isDetected) {
        this.isDetected = isDetected;
    }

    
}
