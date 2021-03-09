using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int health;
    public string enemyName;

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

    //hash codes for the animations
    public int isWalkingHash;

    protected void initStart(string enemyName, int health) {
        gameObject.layer = LayerMask.NameToLayer("Enemy");
        setEnemyName(enemyName);
        setHealth(health);

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
    }

    
    private void setAnimationHashCodes() {
        isWalkingHash = Animator.StringToHash("isWalking");
    }

    private void getCurrentAnimationPlaying() {
        walkAnimation = animator.GetCurrentAnimatorStateInfo(0).IsTag("Walk");
    }

    private void updateGravity() {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }
    }

    void groundCheck() {
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

    public void die() {
        if(health <= 0) {
            Destroy(gameObject);
        }
    }

    public void goTowardsEnemy() {
        
        transform.LookAt(playerTransform);

        if (Vector3.Distance(transform.position, playerTransform.position) >= MinDistance) {
            animator.SetBool(isWalkingHash, true);
            transform.position += transform.forward * moveSpeed * Time.deltaTime;

            if (Vector3.Distance(transform.position, playerTransform.position) <= MaxDistance) {
                //implement Attack
            }
        }
        else {
            animator.SetBool(isWalkingHash, false);
        }
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

    
}
