using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree_Dialog : Dialog {
    // Start is called before the first frame update
    void Awake() {
        initStart();
        newDialogLine("I wish i could climb up there..."); moodSprite(0); //0=normal 1=sad 2=angry
        newDialogLine("But I'm afaid of heights"); moodSprite(1); //0=normal 1=sad 2=angry
    }

    // Update is called once per frame
    void Update() {
        dialog();
    }


}