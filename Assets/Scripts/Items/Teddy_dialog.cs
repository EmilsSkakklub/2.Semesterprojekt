using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teddy_dialog : Dialog {
    GameObject teddy;

    void Awake() {
        teddy = GameObject.Find("Teddy");
        initStart(false);
        newDialogLine("I love Teddy. He always makes me feel safe.", 0);//0=normal 1=sad 2=angry

    }
        // Update is called once per frame
        void Update() {
        if (!inventory.getGotBackPack()) {
            dialog();
        }

        if (interaction.getStartInteraction() && inventory.getGotBackPack()) {
            inventory.setGotTeddy(true);
            teddy.SetActive(false);
        }

    }
}
