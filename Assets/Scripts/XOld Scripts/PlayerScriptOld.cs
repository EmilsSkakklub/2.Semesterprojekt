using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScriptOld : MonoBehaviour
{
    Animator animator;
    public Rigidbody rb;
    private CharacterController controller;
    public LayerMask groundMask;

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


    

    Vector3 velocity;

    private float speed = 1f;
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;
    public float jumpHeight = 3f;

    public bool isGrounded;
    public bool jumpNotReady;

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
        Movement();
        
    }

    //method for initialisation
    private void init() {
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();

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
        isHitHash = Animator.StringToHash("isHit");
        isDeadHash = Animator.StringToHash("isDead");
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

    //movement method
    private void Movement() {
        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Reduced backwards walking speed
        if (backwardPressed) {
            speed = 0.6f;
        } else {
            speed = 1f;
        }

        //can't move while attacking or being hit
        if (!(animator.GetCurrentAnimatorStateInfo(0).IsTag("1") || 
            animator.GetCurrentAnimatorStateInfo(0).IsTag("2") || 
            animator.GetCurrentAnimatorStateInfo(0).IsTag("3") || 
            /*animator.GetCurrentAnimatorStateInfo(0).IsTag("4") ||*/       //So it wont stop the mid air attack
            animator.GetCurrentAnimatorStateInfo(0).IsTag("Hit") ||
            animator.GetCurrentAnimatorStateInfo(0).IsTag("Dead"))) {


            controller.Move(move * speed * Time.deltaTime);

            //jump
            if (Input.GetButtonDown("Jump") && isGrounded && !jumpNotReady) {
                jumpNotReady = true;
                StartCoroutine(Jump());
            }

            //crouch
            if (crouchPressed) {
                controller.Move(move * speed * -0.5f * Time.deltaTime);
            }
            //sprint
            if (runPressed && !backwardPressed) {
                controller.Move(move * speed * 1.5f * Time.deltaTime);
            }
        }



        //REMOVE LATER!!!!!!! 
        //press f to get hit
        if (Input.GetKeyDown(KeyCode.F)) {
            animator.SetBool(isHitHash, true);
        }
        else {
            animator.SetBool(isHitHash, false);
        }
        //press g to die
        if (Input.GetKeyDown(KeyCode.G)) {
            animator.SetBool(isDeadHash, true);
        }
        else {
            animator.SetBool(isDeadHash, false);
        }

    }

    //jump method
    IEnumerator Jump() {
        animator.SetBool(isJumpingHash, true);
        yield return new WaitForSeconds(0.305f);
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        animator.SetBool(isJumpingHash, false);
        yield return new WaitForSeconds(0.195f);
        jumpNotReady = false;
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





    //getters and setters
    public Animator getAnimator() {
        return animator;
    }
}
