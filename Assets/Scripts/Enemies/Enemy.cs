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

    //hash codes for the animations
    private int isWalkingHash;
    private int isDeadHash;
    private int isAttackingHash;

    protected void initStart(string enemyName, int health, float deathAnimTimer) {
        gameObject.layer = LayerMask.NameToLayer("Enemy");
        setEnemyName(enemyName);
        setHealth(health);
        setRemoveTimer(deathAnimTimer);

        animator = GetComponent<Animator>();
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        controller = GetComponent<CharacterController>();

        setAnimationHashCodes();
    }

    protected void initUpdate() {
        getCurrentAnimationPlaying();
        updateGravity();
        groundCheck();
        goTowardsEnemy();
        die();


        //ved ikke hvorden den ikke virker hvis jeg smider den ned i en method
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }

  
    
    private void setAnimationHashCodes() {
        isWalkingHash = Animator.StringToHash("isWalking");
        isDeadHash = Animator.StringToHash("isDead");
        isAttackingHash = Animator.StringToHash("isAttacking");

    }

    private void getCurrentAnimationPlaying() {
        walkAnimation = animator.GetCurrentAnimatorStateInfo(0).IsTag("Walk");
        deadAnimation = animator.GetCurrentAnimatorStateInfo(0).IsTag("Dead");
        attackAnimation = animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack");
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

    public void takeDamage(int damage) {
       health -= damage;
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
        if (!isDead) {

            transform.LookAt(playerTransform);
            if (Vector3.Distance(transform.position, playerTransform.position) >= MinDistance) {
                animator.SetBool(isWalkingHash, true);
                transform.position += transform.forward * moveSpeed * Time.deltaTime;

                if (Vector3.Distance(transform.position, playerTransform.position) <= MaxDistance) {
                    attack();
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
        animator.SetBool(isAttackingHash, true);
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
