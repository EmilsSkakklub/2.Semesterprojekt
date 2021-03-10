using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
    
    public string enemyName;
    public bool isDead;
    public float removeTimer;

    private Animator animator;
    private Transform playerTransform;
    private CharacterController controller;
    private Transform playerCamera;

    private Vector3 velocity;
    private float gravity = -9.81f;
    public bool isGrounded;

    private float moveSpeed = 1f;
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

    //cobat
    public Transform attackpoint;
    private float attackRange = 0.5f;
    public LayerMask playerLayers;
    public bool hit = true;
    private float attackTimer = 0;
    private float attackStart = 0.5f;
    private float attackEnd = 1f;

    public int health;
    public int maxHealth;
    public Slider healthSlider;

    protected void initStart(string enemyName, int maxHealth, float deathAnimTimer) {

        gameObject.layer = LayerMask.NameToLayer("Enemy");
        playerLayers = LayerMask.GetMask("Player");

        setEnemyName(enemyName);
        setMaxHealth(maxHealth);
        setHealth(maxHealth);
        setRemoveTimer(deathAnimTimer);
        

        animator = GetComponent<Animator>();
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        controller = GetComponent<CharacterController>();
        playerCamera = GameObject.Find("Main Camera").GetComponent<Transform>();

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
    }

  
    
    private void setAnimationHashCodes() {
        isWalkingHash = Animator.StringToHash("isWalking");
        isDeadHash = Animator.StringToHash("isDead");
        isAttackingHash = Animator.StringToHash("isAttacking");
        isHitHash = Animator.StringToHash("isHit");

    }

    private void getCurrentAnimationPlaying() {
        walkAnimation = animator.GetCurrentAnimatorStateInfo(0).IsTag("Walk");
        deadAnimation = animator.GetCurrentAnimatorStateInfo(0).IsTag("Dead");
        attackAnimation = animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack");
        hitAnimation = animator.GetCurrentAnimatorStateInfo(0).IsTag("Hit");
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
        if (!isDead && !hitAnimation) {

            transform.LookAt(playerTransform);
            if (Vector3.Distance(transform.position, playerTransform.position) >= MinDistance) {
                animator.SetBool(isWalkingHash, true);
                transform.position += transform.forward * moveSpeed * Time.deltaTime;

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
    }


    private void attack() {
        Collider[] hitPlayer = Physics.OverlapSphere(attackpoint.position, attackRange, playerLayers);
        if (attackAnimation) {
            attackTimer += Time.deltaTime;
        }

        if (attackAnimation && hit && attackTimer > attackStart && attackTimer < attackEnd) {
            foreach (Collider player in hitPlayer) {
                player.GetComponent<PlayerScript>().takeDamage(1);
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



    public void setHealthSlider(int health) {
        healthSlider.value = health;
        healthSlider.transform.rotation = playerCamera.transform.rotation;
    }

    public void setMaxHealthSlider(int health) {
        healthSlider.maxValue = health;
        healthSlider.value = health;
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


    
}
