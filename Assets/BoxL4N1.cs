using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxL4N1 : BoxDialog {
    // Start is called before the first frame update

    Transform player;
    Transform lb;
    void Start() {
        initStart(false);
        newDialogLine("We made it home!", 3);
        newDialogLine("I am never going in there again!", 0);
        newDialogLine("I am sorry about earlier Max.", 1);
        newDialogLine("It's okay Arthur, you came when I needed you.", 3);
        newDialogLine("And Arthur..?", 4);
        newDialogLine("Yes?", 0);
        newDialogLine("I was really scared.", 4);
        newDialogLine("Me too Max.", 1);
        newDialogLine("Let's get inside before the tree men gets us.", 1);
        newDialogLine("Tree men?", 3);
        newDialogLine("Let's just go.", 0);

        player = GameObject.Find("Player").GetComponent<Transform>();
        lb = GameObject.Find("LittleBro2").GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update() {
        initUpdate();
        if (interaction.getStartInteraction()) {
            player.LookAt(lb);
        }

    }
}
