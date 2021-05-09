using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lb_move : MonoBehaviour
{
    Interaction interaction;
    PlayerScript ps;
    Transform p;
    GameManager gm;
    Transform lbTrans;
    Animator animator;
    AudioManager audioManager;

    GameObject g1;
    GameObject g2;
    GameObject g3;
    GameObject g4;
    GameObject g5;
    GameObject g6;
    GameObject g10;

    bool check1 = false;
    bool check2 = false;
    bool check3 = false;
    bool check4 = false;
    bool check5 = false;
    bool check6 = false;
    bool check7 = false;
    bool check8 = false;
    bool switchNow = false;

    private Vector3 currentPos;
    private Vector3 oldPos;

    public bool walkingAnimation;
    public bool runningAnimation;

    public int isWalkingHash;
    public int isRunningHash;


    // Start is called before the first frame update
    void Start()
    {
        interaction = GetComponent<Interaction>();
        ps = GameObject.Find("Player").GetComponent<PlayerScript>();
        p = GameObject.Find("Player").GetComponent<Transform>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        lbTrans = GameObject.Find("LittleBro").GetComponent<Transform>();
        animator = GameObject.Find("LB@Idle").GetComponent<Animator>();
        audioManager = GameObject.Find("GameManager").GetComponent<AudioManager>();

        g1 = GameObject.Find("Goal1");
        g2 = GameObject.Find("Goal2");
        g3 = GameObject.Find("Goal3");
        g4 = GameObject.Find("Goal4");
        g5 = GameObject.Find("Goal5");
        g6 = GameObject.Find("Goal6");
        g10 = GameObject.Find("Goal10");

        isWalkingHash = Animator.StringToHash("Walking");
        isRunningHash = Animator.StringToHash("Running");

        interaction.setStartInteraction(true);
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(LbMoving());
        SoundManager();

        walkingAnimation = animator.GetCurrentAnimatorStateInfo(0).IsTag("Walking");
        runningAnimation = animator.GetCurrentAnimatorStateInfo(0).IsTag("Running");

        
    }

    IEnumerator LbMoving() {
        //checks if object is moving and stops animation of not
        currentPos = gameObject.transform.position;
        if (currentPos == oldPos) {
            animator.SetBool(isWalkingHash, false);
            animator.SetBool(isRunningHash, false);
        }
        oldPos = currentPos;


        if (!interaction.getStartInteraction()) {
            if (gm.StoryNumber == 0) {
                if (CalcDist1() > 1 && !check1) {
                    lbTrans.transform.LookAt(g1.transform);
                    lbTrans.transform.Translate(Vector3.forward * Time.deltaTime * 2.5f);
                    animator.SetBool(isRunningHash, true);
                    if (CalcDist1() <= 1) {
                        
                        check1 = true;
                    }
                } else {
                    if (CalcDist2() > 1) {
                        lbTrans.transform.LookAt(g2.transform);
                        lbTrans.transform.Translate(Vector3.forward * Time.deltaTime * 2.5f);

                    }
                    if (CalcDist2() <= 1) {
                        lbTrans.transform.LookAt(p);
                        animator.SetBool(isRunningHash, false);
                    }
                }
            }
            if (gm.StoryNumber == 0.02f) {
                if (!check5) {
                    interaction.setStartInteraction(true);
                    check5 = true;
                }


                if (CalcDist3() > 1 && !check2) {
                    lbTrans.transform.LookAt(g3.transform);
                    lbTrans.transform.Translate(Vector3.forward * Time.deltaTime * 1.5f);
                    animator.SetBool(isWalkingHash, true);
                    if (CalcDist3() <= 1) {
                        animator.SetBool(isWalkingHash, false);
                        yield return new WaitForSeconds(1.5f);
                        check2 = true;
                    }
                } else if (check2 && !check3 && CalcDist4() > 1) {
                    lbTrans.transform.LookAt(g4.transform);
                    lbTrans.transform.Translate(Vector3.forward * Time.deltaTime * 1.5f);
                    animator.SetBool(isWalkingHash, true);
                    if (CalcDist4() <= 1) {
                        animator.SetBool(isWalkingHash, false);
                        yield return new WaitForSeconds(1.5f);
                        check3 = true;
                    }
                } else if (check2 && check3 && !check4 && CalcDist5() > 0.2f) {
                    lbTrans.transform.LookAt(g5.transform);
                    lbTrans.transform.Translate(Vector3.forward * Time.deltaTime * 1.5f);
                    animator.SetBool(isWalkingHash, true);
                    if (CalcDist5() <= 0.2f && gm.StoryNumber == 0.02f) {
                        animator.SetBool(isWalkingHash, false);
                        gm.StoryNumber = 0.03f;
                        gm.CheckStory = true;
                        check4 = true;
                    }
                }
            }
            if (gm.StoryNumber == 0.04f) {
                if (CalcDist6() > 1) {
                    lbTrans.transform.LookAt(g6.transform);
                    lbTrans.transform.Translate(Vector3.forward * Time.deltaTime * 2.5f);
                    animator.SetBool(isRunningHash, true);
                }
                if (CalcDist6() <= 1 && !check6) {
                    lbTrans.transform.LookAt(p.transform);
                    animator.SetBool(isRunningHash, false);
                    check6 = true;
                }
            }
            

            if (gm.StoryNumber == 0.08f) {
                if(CalcDist10() > 1) {
                    lbTrans.transform.LookAt(g10.transform);
                    lbTrans.transform.Translate(Vector3.forward * Time.deltaTime * 1.5f);
                    animator.SetBool(isWalkingHash, true);
                }
                if(CalcDist10() < 1 && !check8) {
                    animator.SetBool(isWalkingHash, false);
                    gm.StoryNumber = 0.09f;
                    gm.CheckStory = true;
                    check8 = true;
                }
            }


        }
        if (gm.StoryNumber == 0.07f) {
            if (interaction.getStartInteraction()) {
                switchNow = true;
                while (interaction.getStartInteraction()) yield return null;
            }
            if (switchNow) {
                gm.StoryNumber = 0.08f;
                gm.CheckStory = true;
            }
        }
        if (gm.StoryNumber == 0.06f && !check7) {
            interaction.setStartInteraction(true);
            check7 = true;
        }
        yield return null;
    }



    private bool walkingSound;
    private bool runningSound;


    private void SoundManager() {
        if(walkingAnimation && !walkingSound) {
            audioManager.Play("LBWalk", true, 0.01f, 1f);
            audioManager.Stop("LBRun");
            walkingSound = true;
        }
        else if (!walkingAnimation) {
            audioManager.Stop("LBWalk");
            walkingSound = false;
        }

        if(runningAnimation && !runningSound) {
            audioManager.Play("LBRun", true, 0.03f, 1.5f);
            audioManager.Stop("LBWalk");
            runningSound = true;
        }
        else if (!runningAnimation) {
            audioManager.Stop("LBRun");
            runningSound = false;
        }
    }




    float CalcDist1() {
        return Vector3.Distance(transform.position, g1.transform.position);
    }
    float CalcDist2() {
        return Vector3.Distance(transform.position, g2.transform.position);
    }
    float CalcDist3() {
        return Vector3.Distance(transform.position, g3.transform.position);
    }
    float CalcDist4() {
        return Vector3.Distance(transform.position, g4.transform.position);
    }
    float CalcDist5() {
        return Vector3.Distance(transform.position, g5.transform.position);
    }
    float CalcDist6() {
        return Vector3.Distance(transform.position, g6.transform.position);
    }
    float CalcDist10() {
        return Vector3.Distance(transform.position, g10.transform.position);
    }
}
