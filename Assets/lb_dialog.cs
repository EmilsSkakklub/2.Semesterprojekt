using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lb_dialog : Dialog {
    // Start is called before the first frame update
    void Awake() {
        initStart();
        newDialogLine("Fra nu af vil jeg tiltales helten Ploug"); moodSprite(0); //0=normal 1=sad 2=angry
        newDialogLine("Du skulle bare ændre få ting i koden"); moodSprite(0); //0=normal 1=sad 2=angry
        newDialogLine(":))))))))))))))))))))))))))))))))"); moodSprite(1); //0=normal 1=sad 2=angry

    }

    // Update is called once per frame
    void Update() {
        dialog();
    }


}
