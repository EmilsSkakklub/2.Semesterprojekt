using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;
    Transform cam;

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
    public bool isJumping;
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

    //movement
    Vector3 velocity;
    public bool isGrounded;
    public bool jumpNotReady;
    float jumpHeight = .75f;
    float gravity = -9.81f;




    private void Start() {
        initiate();
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void FixedUpdate() {
        GroundCheck();
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
        cam = GameObject.Find("Main Camera").GetComponent<Transform>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isJumpingHash = Animator.StringToHash("isJumping");
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
        //Gets Axis from Input Manager
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        //prevents from using y-axis (Normalized so you wont move faster when pressing W && D/A)
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;


        if (direction.magnitude >= 0.1f) {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref TurnSmoothVelocity, SmoothTurn);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        // jump
        if (Input.GetButtonDown("Jump") && isGrounded) {
            StartCoroutine("Jump");
            
            //StartCoroutine(Jump());
        }

        //gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if(isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }
    }

    //method for checking if the player is grounded
    void GroundCheck() {
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
    }

    private void animatePlayer() {
        //walking animation
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

        //jumping animation
        /*if (jumpPressed) {
            animator.SetBool(isJumpingHash, true);
        }
        else if (!jumpPressed){
            animator.SetBool(isJumpingHash, false);
        }*/


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

        //jump attack animation
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("4")) {
            animator.SetBool(isAttack1Hash, false);
        }


    }
}
