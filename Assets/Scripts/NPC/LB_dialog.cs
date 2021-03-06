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

    public Sprite[] LB_sprites;
    int i = 1;
    int k = 0;


    string Dialog1 = "It is hot to play out in the sun. Mom said we need sunscreen!";
    string Dialog2 = "You can change my mood by pressing 'm' on your keyboard";
    string Dialog3 = "I'm a gnome and you've been gnomed";
    string Dialog4 = "Never gonna give you up, never gonna let you down";

    void Start()
    {
        interaction = GetComponent<Interaction>();
        sr = GameObject.Find("TextBubble").GetComponent<SpriteRenderer>();
        textBubble = GameObject.Find("TextBubble");
        dialogtext = GameObject.Find("DialogText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeSprite(k);
        Dialog();

    }
    void Dialog() {
        switch (i) {
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
            interaction.StartInteraction = false;
            i = 1;
            break;
        }

        if (interaction.StartInteraction) {
            textBubble.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E)) {
                if (i < 6) {
                    i++;
                }
            }
            if (Input.GetKeyDown(KeyCode.M)) {
                if (k == 0) {
                    k = 1;
                } else if (k == 1) {
                    k = 0;
                }
            }
        }
    }
    void ChangeSprite(int mood) {
        sr.sprite = LB_sprites[mood];
    }
}
