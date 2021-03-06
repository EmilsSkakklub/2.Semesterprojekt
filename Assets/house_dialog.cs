using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class house_dialog : Dialog {
    // Start is called before the first frame update
    void Awake() {
        initStart();
        newDialogLine("Once coded, I can go in my house again");
        newDialogLine("I really need to pee...");
    }

    // Update is called once per frame
    void Update() {
        dialog();
    }


}
