using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teddy_interact : Dialog {
    bool checkOnce = false;
    // Start is called before the first frame update
    void Awake() {
        initStart(false);
    }

    // Update is called once per frame
    void Update() {
        if (!checkOnce) {
            if (gm.StoryNumber == 0.09f) {
                dialogLines.Clear();
                moodSprites.Clear();
                newDialogLine("Woah! It's a big maze.", 3);  //0=normal 1=sad 2=angry
                newDialogLine("You talk?", 0);
                newDialogLine("Hush! We need to find your brother", 3);  //0=normal 1=sad 2=angry
                checkOnce = true;
            }
        }

        dialog();
    }


}
