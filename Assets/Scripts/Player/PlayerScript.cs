using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;
    Transform cam;

    public LayerMask groundMask;

    private float speed = 2f;
    private float SmoothTurn = 0.1f;

    private float TurnSmoothVelocity;

    private bool crouchingToggle = false;

    //input booleans
    public bool forwardPressed;
    public bool runPressed;
    public bool backwardPressed;
    public bool leftPressed;
    public bool rightPressed;
    public bool jumpPressed;
    public bool attackPressed;
    public bool crouchPressed;


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
    int isCrouchingHash;
    int isHitHash;
    int isDeadHash;

    //movement
    Vector3 velocity;
    public bool isGrounded;
    public bool jumpNotReady;
    float jumpHeight = .75f;
    float gravity = -9.81f;

    public bool dash1Ready;
    public bool dash2Ready;
    public bool dash3Ready;
    public float dashSpeed = 10f;
    public float dashTime = .1f;







    private void Start() {
        initiate();
       
    }
    private void FixedUpdate() {
        GroundCheck();
    }
    void Update()
    {
        movement();
        animatePlayer();
        inputManager();


        /*
        StartCoroutine("Dash1");
        StartCoroutine("Dash2");
        StartCoroutine("Dash3");
        //TESTKODE - FJERN SENERE
        if (Input.GetKeyDown(KeyCode.F)) {
            animator.SetBool(isHitHash, true);
        }
        if (Input.GetKeyDown(KeyCode.G)) {
            animator.SetBool(isDeadHash, true);
        }
        else {
            animator.SetBool(isHitHash, false);
            animator.SetBool(isDeadHash, false);
        }*/
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
        isCrouchingHash = Animator.StringToHash("isCrouching");

        Cursor.lockState = CursorLockMode.Locked;
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
        crouchingToggle = false;
    }

    private void animatePlayer() {

        //checks if idle / runnig / walking
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("idle")) {

            dash1Ready = true;
            dash2Ready = true;
            dash3Ready = true;
        }

        //walking animation
        if((forwardPressed || backwardPressed || leftPressed || rightPressed)) {
            animator.SetBool(isWalkingHash, true);
        }
        else {
            animator.SetBool(isWalkingHash, false);
        }

        //running animation
        if ((forwardPressed || backwardPressed || leftPressed || rightPressed) && runPressed) {
            animator.SetBool(isRunningHash, true);
            crouchingToggle = false;
            speed = 4;
        }
        else{
            animator.SetBool(isRunningHash, false);
            speed = 2;
        }

        //crouching animation
        if (crouchPressed) {
            crouchingToggle = !crouchingToggle;
        }

        if (crouchingToggle) {
            animator.SetBool(isCrouchingHash, true);

            //Corrects hitbox and speed
            controller.height = 0.1f;
            controller.center = new Vector3(0f, 0.05f, 0f);
            speed = 1;
        }
        else if (!crouchingToggle) {
            animator.SetBool(isCrouchingHash, false);

            //Corrects hitbox and speed
            controller.height = 0.14f;
            controller.center = new Vector3(0f, 0.075f, 0f);
        }


        //attack animation
        //attack 1
        if (attackPressed && !animator.GetCurrentAnimatorStateInfo(0).IsTag("1")
                          && !animator.GetCurrentAnimatorStateInfo(0).IsTag("2")
                          && !animator.GetCurrentAnimatorStateInfo(0).IsTag("3")) {
            animator.SetBool(isAttack1Hash, true);
            crouchingToggle = false;
            /*
            if (dash1Ready && !animator.IsInTransition(0)) {
                StartCoroutine("Dash1");
                dash1Ready = false;
            }*/

        }


        //attack 2
        if (attackPressed && animator.GetCurrentAnimatorStateInfo(0).IsTag("1")) {
            animator.SetBool(isAttack2Hash, true);

            /*if (dash2Ready && !animator.IsInTransition(0)) {
                StartCoroutine("Dash2");
                dash2Ready = false;
            }*/
        }
        


        //attack 3
        if (attackPressed && animator.GetCurrentAnimatorStateInfo(0).IsTag("2")) {
            animator.SetBool(isAttack3Hash, true);
            /*if (dash3Ready && !animator.IsInTransition(0)) {
                StartCoroutine("Dash3");
                dash3Ready = false;
            }*/
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
    /*IEnumerator Dash1() {
        bool attack1playing = animator.GetCurrentAnimatorStateInfo(0).IsTag("1");
        yield return new WaitUntil(() => attack1playing);

        yield return new WaitForSeconds(0.20f);

        float startTime = Time.time; // need to remember this to know how long to dash
        while (Time.time < startTime + dashTime) {
            controller.Move(transform.forward * dashSpeed * Time.deltaTime);
            yield return null; // this will make Unity stop here and continue next frame
            }
        
        
    }
    IEnumerator Dash2() {
        bool attack2playing = animator.GetCurrentAnimatorStateInfo(0).IsTag("2");
        yield return new WaitUntil(() => attack2playing);
        yield return new WaitForSeconds(0.50f);
        float startTime = Time.deltaTime; // need to remember this to know how long to dash
        while (Time.time < startTime + dashTime) {
            controller.Move(transform.forward * dashSpeed * Time.deltaTime);
            yield return null; // this will make Unity stop here and continue next frame
        }
    }
    IEnumerator Dash3() {
        bool attack3playing = animator.GetCurrentAnimatorStateInfo(0).IsTag("3");
        yield return new WaitUntil(() => attack3playing);

        yield return new WaitForSeconds(0.50f);
        float startTime = Time.time; // need to remember this to know how long to dash
        while (Time.time < startTime + dashTime) {
            controller.Move(transform.forward * dashSpeed * Time.deltaTime);
            yield return null; // this will make Unity stop here and continue next frame
        }
    }*/

    //getters and setters
    public Animator getAnimator() {
        return animator;
    }
}





