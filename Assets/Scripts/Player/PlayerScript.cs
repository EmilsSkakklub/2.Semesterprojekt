using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Animator animator;

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



    private CharacterController controller;
    public LayerMask groundMask;

    Vector3 velocity;

    private float speed = 1f;
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;
    public float jumpHeight = 3f;

    public bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        init();
        
    }

    // Update is called once per frame
    private void FixedUpdate() {
        GroundCheck();
    }
    
    private void Update()
    {
        inputManager();
        animateCharacter();
        movement();
        
    }


    //movement method
    private void movement() {
        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        //jump
        if (Input.GetButtonDown("Jump") && isGrounded) {
            Invoke("Jump", 0.305f);
            isGrounded = false;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //crouch
        if (crouchPressed) {
            controller.Move(move * speed * -0.5f * Time.deltaTime);
        }
        //sprint
        if (runPressed) {
            controller.Move(move * speed * 1.5f * Time.deltaTime);
        }
    }

    //jump method
    void Jump() {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    //method for checking if the player is grounded
    void GroundCheck() {

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.07f + 0.1f)) {
            isGrounded = true;
        } else {
            isGrounded = false;
        }
    }

    //method for initialisation
    private void init() {
        controller = GetComponent<CharacterController>();

        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isJumpingHash = Animator.StringToHash("isJump");
        isBackwardsHash = Animator.StringToHash("isBackwards");
        isAttack1Hash = Animator.StringToHash("isAttack1");
        isAttack2Hash = Animator.StringToHash("isAttack2");
        isAttack3Hash = Animator.StringToHash("isAttack3");
        isLeftHash = Animator.StringToHash("isLeft");
        isRightHash = Animator.StringToHash("isRight");
    }

    //method to check the players input
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

    //Method to do the animations of the character
    private void animateCharacter() {
        //walk animation
        if (forwardPressed && !isWalking) {
            animator.SetBool(isWalkingHash, true);
        }
        else if (!forwardPressed) {
            animator.SetBool(isWalkingHash, false);
        }

        //run animation
        if ((runPressed && forwardPressed) && !isRunning) {
            animator.SetBool(isRunningHash, true);
        }
        else if ((!runPressed || !forwardPressed)) {
            animator.SetBool(isRunningHash, false);
        }

        //walk backwards animation
        if (backwardPressed) {
            animator.SetBool(isBackwardsHash, true);
        }
        else if (!backwardPressed) {
            animator.SetBool(isBackwardsHash, false);
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

        //jump attack animation
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("4")) {
            animator.SetBool(isAttack1Hash, false);
        }


        //jump animation
        if (jumpPressed) {
            animator.SetBool(isJumpingHash, true);
        }
        else {
            animator.SetBool(isJumpingHash, false);
        }


        //strafe animation
        if (leftPressed && !rightPressed) {
            animator.SetBool(isLeftHash, true);
        }
        if (rightPressed && !leftPressed) {
            animator.SetBool(isRightHash, true);
        }
        if (!rightPressed && !leftPressed) {
            animator.SetBool(isLeftHash, false);
            animator.SetBool(isRightHash, false);
        }
        if (rightPressed && leftPressed) {
            animator.SetBool(isLeftHash, false);
            animator.SetBool(isRightHash, false);
        }

    }
}
