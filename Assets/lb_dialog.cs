using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lb_dialog : Dialog {
    // Start is called before the first frame update
    void Awake() {
        initStart();
        newDialogLine("Fra nu af vil jeg tiltales helten Ploug");
        newDialogLine("Du skulle bare ændre få ting i koden");
        newDialogLine(":))))))))))))))))))))))))))))))))");

    }

    // Update is called once per frame
    void Update() {
        dialog();
    }


}
