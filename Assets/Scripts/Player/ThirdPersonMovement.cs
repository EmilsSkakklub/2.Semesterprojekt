using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    private CharacterController controller;
    private Transform Camera;
    private Animator animator;

    public LayerMask groundMask;

    private float speed = 2f;
    private float SmoothTurn = 0.1f;

    private float TurnSmoothVelocity;


    //input booleans
    public bool forwardPressed;
    public bool runPressed;
    public bool backwardPressed;
    public bool leftPressed;
    public bool rightPressed;
    public bool jumpPressed;
    public bool attackPressed;
    public bool crouchPressed;

    //bool to check if what the player is doing
    public bool isWalking;
    public bool isRunning;
    public bool isjumping;
    public bool isAttacking1;
    public bool isAttacking2;
    public bool isAttacking3;
    bool isBackwards;
    bool isTurningRight;
    bool isTurningLeft;
    public bool isHit;
    public bool isDead;

    //hash codes for  the animator 
    int isWalkingHash;
    int isRunningHash;
    int isJumpingHash;
    int isBackwardsHash;
    int isLeftHash;
    int isRightHash;
    int isAttack1Hash;
    int isAttack2Hash;
    int isAttack3Hash;
    int isHitHash;
    int isDeadHash;



    private void Start() {
        initiate();
    }

    void Update()
    {
        movement();
        animatePlayer();
        inputManager();
    }



    private void initiate() {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        Camera = GameObject.Find("Main Camera").GetComponent<Transform>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isJumpingHash = Animator.StringToHash("isJump");
        isBackwardsHash = Animator.StringToHash("isBackwards");
        isAttack1Hash = Animator.StringToHash("isAttack1");
        isAttack2Hash = Animator.StringToHash("isAttack2");
        isAttack3Hash = Animator.StringToHash("isAttack3");
        isLeftHash = Animator.StringToHash("isLeft");
        isRightHash = Animator.StringToHash("isRight");
        isHitHash = Animator.StringToHash("isHit");
        isDeadHash = Animator.StringToHash("isDead");
    }

    private void inputManager() {
        forwardPressed = Input.GetKey(KeyCode.W);
        runPressed = Input.GetKey(KeyCode.LeftShift);
        backwardPressed = Input.GetKey(KeyCode.S);
        leftPressed = Input.GetKey(KeyCode.A);
        rightPressed = Input.GetKey(KeyCode.D);
        jumpPressed = Input.GetKeyDown(KeyCode.Space);
        attackPressed = Input.GetMouseButtonDown(0);
        crouchPressed = Input.GetKeyDown(KeyCode.C);
    }

    private void movement() {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f) {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref TurnSmoothVelocity, SmoothTurn);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
           
                controller.Move(moveDirection.normalized * speed * Time.deltaTime);
            
        }

        
    }

    private void animatePlayer() {
        //walkinf animation
        if(forwardPressed || backwardPressed || leftPressed || rightPressed) {
            animator.SetBool(isWalkingHash, true);
        }
        else {
            animator.SetBool(isWalkingHash, false);
        }

        //running animation
        if (runPressed) {
            animator.SetBool(isRunningHash, true);
            speed = 4;
        }
        else {
            animator.SetBool(isRunningHash, false);
            speed = 2;
        }


        //attack animation
        if (attackPressed && !animator.GetCurrentAnimatorStateInfo(0).IsTag("1")
            && !animator.GetCurrentAnimatorStateInfo(0).IsTag("2")
            && !animator.GetCurrentAnimatorStateInfo(0).IsTag("3")) {
            animator.SetBool(isAttack1Hash, true);
        }
        if (attackPressed && animator.GetCurrentAnimatorStateInfo(0).IsTag("1")) {
            animator.SetBool(isAttack2Hash, true);
        }
        if (attackPressed && animator.GetCurrentAnimatorStateInfo(0).IsTag("2")) {
            animator.SetBool(isAttack3Hash, true);
        }


        if (!attackPressed && animator.GetCurrentAnimatorStateInfo(0).IsTag("1")) {
            animator.SetBool(isAttack1Hash, false);
        }
        if (!attackPressed && animator.GetCurrentAnimatorStateInfo(0).IsTag("2")) {
            animator.SetBool(isAttack2Hash, false);
        }
        if (!attackPressed && animator.GetCurrentAnimatorStateInfo(0).IsTag("3")) {
            animator.SetBool(isAttack3Hash, false);
        }

    }
}
