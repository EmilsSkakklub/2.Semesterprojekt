using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Dialog : MonoBehaviour
{
    public Interaction interaction;
    public SpriteRenderer spriteRenderer;
    public GameObject textBubble;
    public Text dialogText;
    public PlayerScript playerScript;

    public Sprite[] sprites;
    public List<string> dialogLines = new List<string>();

    public int dialogNumber;
    public int maxNumber;

    protected void initStart() {
        interaction = GetComponent<Interaction>();
        textBubble = GameObject.Find("TextBubble");
        spriteRenderer = GameObject.Find("TextBubble").GetComponent<SpriteRenderer>();
        dialogText = GameObject.Find("DialogText").GetComponent<Text>();
        playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();

        setDialogNumber(0);

        newDialogLine("Hello There, how are you");
        newDialogLine("Hello There, how are you there");
    }

    protected void dialog() {
        
        maxNumber = dialogLines.Count;
        if(dialogNumber <= maxNumber) {
            dialogText.text = dialogLines[dialogNumber];
        }
        else if(dialogNumber > maxNumber) {
            textBubble.SetActive(false);
            interaction.setStartInteraction(false);
            playerScript.setInDialog(false);
        }
        



        if (interaction.getStartInteraction()) {
            spriteRenderer.sprite = sprites[1];
            textBubble.SetActive(true);
            playerScript.setInDialog(true);

            if (Input.GetKeyDown(KeyCode.E)) {
                incrementDialogNumber();
            }
        }

    }

    protected void newDialogLine(string line) {
        dialogLines.Add(line);
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
