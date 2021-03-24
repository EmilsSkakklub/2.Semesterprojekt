using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Dialog : MonoBehaviour {
    protected Interaction interaction;
    protected GameManager gm;
    protected Inventory inventory;
    private SpriteRenderer spriteRenderer;
    private GameObject textBubble;
    private Text dialogText;
    private PlayerScript playerScript;

    public Sprite[] sprites;
    public List<string> dialogLines = new List<string>();
    public List<int> moodSprites = new List<int>();

    private Transform player;
    private Transform NPC;
    private Quaternion defaultRotation;

    private bool isNPC;

    private int dialogNumber;
    private int maxNumber;

    protected void initStart(bool isNPC) {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
        interaction = GetComponent<Interaction>();
        textBubble = GameObject.Find("TextBubble");
        spriteRenderer = GameObject.Find("TextBubble").GetComponent<SpriteRenderer>();
        dialogText = GameObject.Find("DialogText").GetComponent<Text>();
        playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();

        player = GameObject.Find("Player").GetComponent<Transform>();
        NPC = transform.parent.gameObject.transform;
        defaultRotation = NPC.GetComponent<Transform>().rotation;

        setIsNPC(isNPC);
        setDialogNumber(0);
    }

    protected void dialog() {

        maxNumber = dialogLines.Count;

        if (dialogNumber == maxNumber) {
            textBubble.SetActive(false);
            interaction.setStartInteraction(false);
            playerScript.setInDialog(false);
            setDialogNumber(0);
        }
        if (interaction.getStartInteraction()) {
            textBubble.SetActive(true);
            playerScript.setInDialog(true);
            playerScript.lookAtTarget(this.transform);
            if (Input.GetKeyDown(KeyCode.E)) {
                incrementDialogNumber();
            }
            if (dialogNumber < maxNumber) {
                dialogText.text = dialogLines[dialogNumber];
                spriteRenderer.sprite = sprites[moodSprites[dialogNumber]];
            }
        }

        //look at player if object is NPC
        if (isNPC && interaction.getStartInteraction()) {
            lookAtTarget(player);
        }
        if (!interaction.getStartInteraction()) {
            NPC.rotation = Quaternion.Slerp(NPC.rotation, defaultRotation, Time.deltaTime * 2);
        }

    }

    protected void newDialogLine(string line, int mood) {
        dialogLines.Add(line);
        moodSprites.Add(mood);
    }

    private void lookAtTarget(Transform target) {
        Vector3 lookPos = target.position - transform.position;
        lookPos.y = 0;
        Quaternion rotate = Quaternion.LookRotation(lookPos);
        NPC.rotation = Quaternion.Slerp(transform.rotation, rotate, Time.deltaTime * 2);
    }


    //getters and setters
    public int getDialogNumber() {
        return dialogNumber;
    }
    public void setDialogNumber(int dialogNumber) {
        this.dialogNumber = dialogNumber;
    }
    public void incrementDialogNumber() {
        dialogNumber++;
    }

    public bool getIsNPC() {
        return isNPC;
    }
    public void setIsNPC(bool isNPC) {
        this.isNPC = isNPC;
    }
}
