using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hedgehole_dialog : Dialog {
    bool checkOnce = false;
    // Start is called before the first frame update
    void Awake() {
        initStart(false);
        newDialogLine("I'm too scared to go in there...", 0);
    }

    // Update is called once per frame
    void Update() {
            if (!checkOnce) {
                if (gm.StoryNumber == 0.09f) {
                    dialogLines.Clear();
                    moodSprites.Clear();
                    newDialogLine("It looks scary in there.", 1);  //0=normal 1=sad 2=angry
                    newDialogLine("I am going to need my backback.", 0);
                    newDialogLine("And I should bring Teddy, he always knows what to do.", 0);  //0=normal 1=sad 2=angry
                    checkOnce = true;
                }
            }
            
        dialog();
    }


}
