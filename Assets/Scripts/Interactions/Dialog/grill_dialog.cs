using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grill_dialog : Dialog {
    // Start is called before the first frame update
    void Awake() {
        initStart(false);
        newDialogLine("I can still smell the burgers from yesterday!",0); //MOOD: 0=normal 1=sad 2=angry

    }

    // Update is called once per frame
    void Update() {
        dialog();
    }


}
