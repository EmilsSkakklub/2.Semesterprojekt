using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxL1N5 : BoxDialog
{
    // Start is called before the first frame update
    void Start()
    {
        initStart(false);
        newDialogLine("Is that a... stick?", 0);
        newDialogLine("You should take it, might come in usefull.", 3);
        newDialogLine("You can equip it as a weapon if you open your backpack.", 3);
    }

    // Update is called once per frame
    void Update()
    {
        initUpdate();
    }
}
