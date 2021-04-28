using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush_dialog : Dialog
{
    // Start is called before the first frame update
    void Start()
    {
        initStart(false);
        newDialogLine("That's one big bush!", 0);
        newDialogLine("I wonder whats behind it", 1);
        newDialogLine("We probably need something sharp to cut it down", 3);
        newDialogLine("Hmm, maybe I can find something usefull around here...", 1);
    }

    // Update is called once per frame
    void Update()
    {
        dialog();
    }
}
