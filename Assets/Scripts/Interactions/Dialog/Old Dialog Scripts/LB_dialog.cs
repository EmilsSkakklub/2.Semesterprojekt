using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LB_dialog : MonoBehaviour
{
    Interaction interaction;
    SpriteRenderer sr;
    GameObject textBubble;
    Text dialogtext;
    PlayerScript ps;

    public Sprite[] LB_sprites;

    public int dialogSequence = 1;
    public int dialogNumber = 1;
    int moodNumber = 0;

    public bool readyForSequence2;


    string Dialog1 = "It is hot to play out in the sun. Mom said we need sunscreen!";
    string Dialog2 = "You can change my mood by pressing 'm' on your keyboard.";
    string Dialog3 = "If you go and interact with the door, I will say some new stuff when...";
    string Dialog4 = "you get back to me... Rasmus er klog";

    string Dialog5 = "Never gonna give you up";
    string Dialog6 = "Never gonna let you down";
    string Dialog7 = "Never gonna run around and desert you";

    void Awake()
    {
        interaction = GetComponent<Interaction>();
        sr = GameObject.Find("TextBubble").GetComponent<SpriteRenderer>();
        textBubble = GameObject.Find("TextBubble");
        dialogtext = GameObject.Find("DialogText").GetComponent<Text>();
        ps = GameObject.Find("Player").GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeSprite(moodNumber);
        Dialog();

    }
    void Dialog() {
        switch (dialogSequence) {

        //First Dialog
        case 1:
            switch (dialogNumber) {
            case 1:
                dialogtext.text = Dialog1;
                break;
            case 2:
                dialogtext.text = Dialog2;
                break;
            case 3:
                dialogtext.text = Dialog3;
                break;
            case 4:
                dialogtext.text = Dialog4;
                break;
            case 5:
                textBubble.SetActive(false);
                interaction.setStartInteraction(false);
                ps.setInDialog(false);
                dialogNumber = 1;
                readyForSequence2 = true;
                break;
            }
            break;
        
        //Second Dialog
        case 2:
            switch (dialogNumber) {
            case 1:
                dialogtext.text = Dialog5;
                break;
            case 2:
                dialogtext.text = Dialog6;
                break;
            case 3:
                dialogtext.text = Dialog7;
                break;
            case 4:
                textBubble.SetActive(false);
                interaction.setStartInteraction(false);
                ps.setInDialog(false);
                dialogNumber = 1;
                break;
            }
            break;

        }
        

        if (interaction.getStartInteraction()) {
            textBubble.SetActive(true);
            ps.setInDialog(true);

            if (Input.GetKeyDown(KeyCode.E)) {
                    dialogNumber++;

            }
            if (Input.GetKeyDown(KeyCode.M)) {
                if (moodNumber == 0) {
                    moodNumber = 1;
                } else if (moodNumber == 1) {
                    moodNumber = 0;
                }
            }
        }
    }
    void ChangeSprite(int mood) {
        sr.sprite = LB_sprites[mood];
    }
}
