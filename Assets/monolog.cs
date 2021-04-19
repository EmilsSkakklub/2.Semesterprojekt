using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monolog : Dialog
{
    void Awake() {

        initStart(false);
        newDialogLine("Hmm... It has been a while now.", 1);  //0=normal 1=sad 2=angry
        newDialogLine("Maybe I should go check on him?", 0);  //0=normal 1=sad 2=angry
    }

    // Update is called once per frame
    void Update() {
        dialog();
    }

}
