using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleBroScript : Dialog
{
    

    private Transform brotherTransform;
    private Transform playerTransform;

    private Animator animator;
    private bool walkingAnimation;
    private bool runningAnimation;
    private int isWalkingHash;
    private int isRunningHash;

    public bool followPlayer = false;
    private float moveSpeed = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        initStart(true);
        newDialogLine("You found me brother!", 3);

        brotherTransform = GameObject.Find("LittleBro2").GetComponent<Transform>();
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        animator = GameObject.Find("LB@Idle2").GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("Walking");
        isRunningHash = Animator.StringToHash("Running");
    }

    // Update is called once per frame
    void Update()
    {
        dialog();
        FollowPlayer();

        walkingAnimation = animator.GetCurrentAnimatorStateInfo(0).IsTag("Walking");
        runningAnimation = animator.GetCurrentAnimatorStateInfo(0).IsTag("Running");
    }

    private void FollowPlayer() {
        if (interaction.getStartInteraction() && !followPlayer) {
            followPlayer = true;
        }

        if (followPlayer) {
            brotherTransform.LookAt(playerTransform);

            if (Vector3.Distance(brotherTransform.position, playerTransform.position) >= 1f) {
                animator.SetBool(isWalkingHash, true);
                brotherTransform.position += brotherTransform.forward * moveSpeed * Time.deltaTime;
            }
            else {
                animator.SetBool(isWalkingHash, false);
            }
        }
    }

}
