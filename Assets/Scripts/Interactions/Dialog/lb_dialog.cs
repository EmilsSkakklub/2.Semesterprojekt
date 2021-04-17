using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lb_dialog : Dialog {
    // Start is called before the first frame update
    void Awake() {

        initStart(true);
    }

    // Update is called once per frame
    void Update() {
        if (gm.CheckStory) {
            dialogLines.Clear();
            moodSprites.Clear();

            switch (gm.StoryNumber) {
            case 0f:
                newDialogLine("Come on bro!", 3);  //0=normal 1=sad 2=angry
                newDialogLine("Fastest person to the grill wins!", 3);  //0=normal 1=sad 2=angry
                break;

            case 0.01f:
                newDialogLine("It's not fair, you're onlder than me!", 5);  //0=normal 1=sad 2=angry
                newDialogLine("Let's play something else then. How about hide and seek?", 0);  //0=normal 1=sad 2=angry
                newDialogLine("Okay, I'll count to 10.", 3);  //0=normal 1=sad 2=angry 
                break;

            case 0.02f:
                newDialogLine("Ready or not, here I come!", 3);
                break;
            case 0.03f:
                newDialogLine("Are you in here?", 3);
                newDialogLine("Buh!", 2);
                newDialogLine("Arrrh!", 4);
                newDialogLine("*cries*", 4);
                newDialogLine("I'm sorry, please don't tell mom!", 1);
                newDialogLine("Do you want to play some football?", 0);
                newDialogLine("*nods*", 4);
                newDialogLine("Ok, I will go get the ball then", 0);
                break;
            case 0.04f:
                newDialogLine("Shoot here!", 3);
                break;
            case 0.06f:
                newDialogLine("You go get it!", 2);
                newDialogLine("What?! No!", 5);
                newDialogLine("Yes! I'm the oldest!", 2);
                newDialogLine("But, but... What about Mr. Dargon?", 4);
                newDialogLine("Are you scared little brother?", 0);
                newDialogLine("No!?", 4);
                newDialogLine("I'll go get it now!", 5);
                break;
            }
            gm.CheckStory = false;
        }


        dialog();
        if (interaction.getStartInteraction()) {
            inventory.setReadyToPickUpBackpack(true);
        }
    }


}
