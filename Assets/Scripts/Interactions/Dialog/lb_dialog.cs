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
        if (gm.StoryNumber == 0f && gm.CheckStory) {
             dialogLines.Clear();
             moodSprites.Clear();
             newDialogLine("Come on bro, keep up!", 3);  //0=normal 1=sad 2=angry
             gm.CheckStory = false;
            
        }
        if (gm.StoryNumber == 0.01f && gm.CheckStory) {
            
            dialogLines.Clear();
            moodSprites.Clear();
            newDialogLine("It's not fair, you're onlder than me!", 5);  //0=normal 1=sad 2=angry
            newDialogLine("Let's play something else then. How about hide and seek?", 0);  //0=normal 1=sad 2=angry
            newDialogLine("Okay, I'll count to 10.", 3);  //0=normal 1=sad 2=angry
            gm.CheckStory = false;
        }
        




        dialog();
        if (interaction.getStartInteraction()) {
            inventory.setReadyToPickUpBackpack(true);
        }
    }


}
