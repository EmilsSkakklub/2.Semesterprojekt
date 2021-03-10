using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int health;
    public string enemyName;
    public bool isDead;
    public float removeTimer;

    public Animator animator;
    public Transform playerTransform;
    public CharacterController controller;

    public Vector3 velocity;
    public float gravity = -9.81f;
    public bool isGrounded;

    public float moveSpeed = 1f;
    public float MaxDistance = 1f;
    public float MinDistance = 1f;

    //indicate what animation is playing
    public bool walkAnimation;
    public bool deadAnimation;
    public bool attackAnimation;
    public bool hitAnimation;

    //hash codes for the animations
    private int isWalkingHash;
    private int isDeadHash;
    private int isAttackingHash;
    private int isHitHash;

    //cobat
    public Transform attackpoint;
    public float attackRange = 0.3f;
    public LayerMask playerLayers;
    public bool hit1 = true;
    public float attackTimer = 0;
    public float attackStart = 0.5f;
    public float attackEnd = 1f;

    protected void initStart(string enemyName, int health, float deathAnimTimer) {
        gameObject.layer = LayerMask.NameToLayer("Enemy");
        setEnemyName(enemyName);
        setHealth(health);
        setRemoveTimer(deathAnimTimer);

        animator = GetComponent<Animator>();
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        controller = GetComponent<CharacterController>();
        attackpoint = GameObject.Find("EnemyAttackPoint").GetComponent<Transform>();

        setAnimationHashCodes();
    }

    protected void initUpdate() {
        getCurrentAnimationPlaying();
        updateGravity();
        groundCheck();
        goTowardsEnemy();
        die();

        updateAnimations();
        attack();
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

        if (attackAnimation && hit1 && attackTimer > attackStart && attackTimer < attackEnd) {
            foreach (Collider player in hitPlayer) {
                player.GetComponent<PlayerScript>().takeDamage(1);
                Debug.Log(player.name + " hit");
                hit1 = false;
            }
        }
        if (!attackAnimation) {
            hit1 = true;
            attackTimer = 0f;
        }
    }


    private void OnDrawGizmosSelected() {
        if (attackpoint == null) {
            return;
        }
        Gizmos.DrawWireSphere(attackpoint.position, attackRange);
    }



    //getters and setters
    public int getHealth() {
        return health;
    }

    public void setHealth(int health) {
        this.health = health;
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
