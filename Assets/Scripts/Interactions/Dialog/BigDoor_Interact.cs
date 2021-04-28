using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigDoor_Interact : Dialog
{
    // Start is called before the first frame update
    void Start()
    {
        initStart(false);
        newDialogLine("I can't open it, its locked...", 1);
        newDialogLine("Looks like we need a key.", 3);
        newDialogLine("I wonder where it is.", 3);
    }

    // Update is called once per frame
    void Update()
    {
        dialog();
    }
}
