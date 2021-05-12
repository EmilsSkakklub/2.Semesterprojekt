using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxL1N4 : BoxDialog
{
    // Start is called before the first frame update
    void Start()
    {
        initStart(false);
        newDialogLine("Look, bottle of water.", 3); 
        newDialogLine("You should pick it up, might get usefull.", 3);
        newDialogLine("And remember, you can open your backpack by pressing Tab or B.", 3);
    }

    // Update is called once per frame
    void Update()
    {
        initUpdate();
    }
}
