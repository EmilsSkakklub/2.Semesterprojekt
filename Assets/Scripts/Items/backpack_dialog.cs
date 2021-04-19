using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backpack_dialog : Dialog {
    GameObject backpack;

    void Awake() {
        backpack = GameObject.Find("BackpackOnGround");
            initStart(false);
            newDialogLine("My trusty backpack! There's room for everything in here!", 0);//0=normal 1=sad 2=angry
    }

    // Update is called once per frame
    void Update() {
        if (gm.StoryNumber != 0.09f) {
            dialog();
        }
        if (interaction.getStartInteraction() && gm.StoryNumber == 0.09f) {
            inventory.setGotBackPack(true);
            backpack.SetActive(false);
        }
    }

}



