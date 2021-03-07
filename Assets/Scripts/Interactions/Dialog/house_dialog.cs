using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class house_dialog : Dialog {
    // Start is called before the first frame update
    void Awake() {
        initStart();
        newDialogLine("Once coded, I can go in my house again",0); //0=normal 1=sad 2=angry
        newDialogLine("I really need to pee...",1); //0=normal 1=sad 2=angry
    }

    // Update is called once per frame
    void Update() {
        dialog();
    }


}
