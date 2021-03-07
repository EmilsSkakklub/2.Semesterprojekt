using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lb_dialog : Dialog {


    


    // Start is called before the first frame update
    void Awake() {
        initStart(true);
        newDialogLine("Righ now I'm a normal boi",0);  //0=normal 1=sad 2=angry
        newDialogLine("But sometimes I get sad...",1);  //0=normal 1=sad 2=angry
        newDialogLine("And in some cases, I become normal again!",0);  //0=normal 1=sad 2=angry

        
    }

    // Update is called once per frame
    void Update() {
        dialog();
        
    }


}
