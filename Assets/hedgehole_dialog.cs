using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hedgehole_dialog : Dialog {
    // Start is called before the first frame update
    void Awake() {
        initStart();
        newDialogLine("I'm too scared to go in there...");
    }

    // Update is called once per frame
    void Update() {
        dialog();
    }


}
