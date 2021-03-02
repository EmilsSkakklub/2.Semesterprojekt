using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    Animator animator;

    public bool forwardPressed;
    public bool runPressed;
    public bool backwardPressed;
    public bool leftPressed;
    public bool rightPressed;
    public bool jumpPressed;
    public bool attackPressed;


    public bool isWalking;
    public bool isRunning;
    public bool isjumping;
    public bool isAttacking1;
    public bool isAttacking2;
    public bool isAttacking3;
    bool isBackwards;
    bool isTurningRight;
    bool isTurningLeft;


    int isWalkingHash;
    int isRunningHash;
    int isJumpingHash;
    int isBackwardsHash;
    int isLeftHash;
    int isRightHash;
    int isAttack1Hash;
    int isAttack2Hash;
    int isAttack3Hash;

    // Start is called before the first frame update
    void Start() {
        init();
    }

    // Update is called once per frame
    void Update() {
        inputManager();
        animateCharacter();
    }


    private void init() {
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

    private void inputManager() {
        forwardPressed = Input.GetKey(KeyCode.W);
        runPressed = Input.GetKey(KeyCode.LeftShift);
        backwardPressed = Input.GetKey(KeyCode.S);
        leftPressed = Input.GetKey(KeyCode.A);
        rightPressed = Input.GetKey(KeyCode.D);
        jumpPressed = Input.GetKeyDown(KeyCode.Space);
        attackPressed = Input.GetMouseButtonDown(0);
    }


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
        if(backwardPressed) {
            animator.SetBool(isBackwardsHash, true);
        }
        else if (!backwardPressed) {
            animator.SetBool(isBackwardsHash, false);
        }

        
        //attack animation
        if(attackPressed && !animator.GetCurrentAnimatorStateInfo(0).IsTag("1") 
            && !animator.GetCurrentAnimatorStateInfo(0).IsTag("2") 
            && !animator.GetCurrentAnimatorStateInfo(0).IsTag("3")) {
            animator.SetBool(isAttack1Hash, true);
        }
        if(attackPressed && animator.GetCurrentAnimatorStateInfo(0).IsTag("1")) {
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
        if(animator.GetCurrentAnimatorStateInfo(0).IsTag("4")) {
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
        if(rightPressed && !leftPressed) {
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
