using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Dialog : MonoBehaviour
{
    private Interaction interaction;
    private SpriteRenderer spriteRenderer;
    private GameObject textBubble;
    private Text dialogText;
    private PlayerScript playerScript;

    public Sprite[] sprites;
    private List<string> dialogLines = new List<string>();
    public List<int> moodSprites = new List<int>();

    private int dialogNumber;
    private int maxNumber;

    protected void initStart() {
        interaction = GetComponent<Interaction>();
        textBubble = GameObject.Find("TextBubble");
        spriteRenderer = GameObject.Find("TextBubble").GetComponent<SpriteRenderer>();
        dialogText = GameObject.Find("DialogText").GetComponent<Text>();
        playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();

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
        
    }

    protected void newDialogLine(string line) {
        dialogLines.Add(line);
    }
    protected void moodSprite(int mood) {
        moodSprites.Add(mood);

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


}
