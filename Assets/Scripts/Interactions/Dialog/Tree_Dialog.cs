using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree_Dialog : Dialog {
    // Start is called before the first frame update
    void Awake() {
        initStart(false);
        newDialogLine("I wish i could climb up there...",0);  //0=normal 1=sad 2=angry
        newDialogLine("But I'm afaid of heights",1); //0=normal 1=sad 2=angry
    }

    // Update is called once per frame
    void Update() {
        if (interaction.getStartInteraction()) {
            gm.CheckStory = true;
            
            if(gm.StoryNumber <= 0.01f) {
                gm.StoryNumber = 0.01f;
            }

        }
        dialog();

        
    }


}