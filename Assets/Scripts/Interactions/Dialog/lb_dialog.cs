using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lb_dialog : Dialog {


    


    // Start is called before the first frame update
    void Awake() {
        initStart(true);
        newDialogLine("Hello, I am your little brother",3);  //0=normal 1=sad 2=angry
        newDialogLine("Hello little brother, I am your big brother",0);  //0=normal 1=sad 2=angry
        newDialogLine("How are you doing today?",0);  //0=normal 1=sad 2=angry
        newDialogLine("I am doing fine. I am feeling adventurous", 3);  //0=normal 1=sad 2=angry
        newDialogLine("That's cool. That I am not.", 1);  //0=normal 1=sad 2=angry


    }

    // Update is called once per frame
    void Update() {
        dialog();
        
    }


}
