using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lb_dialog : Dialog {
    // Start is called before the first frame update
    void Awake() {
        initStart(true);
        newDialogLine("Hello, I am your little brother",3);  //0=normal 1=sad 2=angry
        newDialogLine("I now allow you to go pick up your backpack",3);  //0=normal 1=sad 2=angry
        newDialogLine("You can also pick up teddy if you want to.", 3);  //0=normal 1=sad 2=angry



    }

    // Update is called once per frame
    void Update() {
        dialog();
        if (interaction.getStartInteraction()) {
            inventory.setReadyToPickUpBackpack(true);
        }
    }


}
