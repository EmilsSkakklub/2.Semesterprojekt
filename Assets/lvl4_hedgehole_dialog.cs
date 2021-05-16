using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lvl4_hedgehole_dialog : Dialog {

    // Start is called before the first frame update
    void Awake() {
        initStart(false);
        newDialogLine("I'm not going in there again any time soon!", 0);
    }

    // Update is called once per frame
    void Update() {
        dialog();
    }


}
