using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxL2N3 : BoxDialog
{
    // Start is called before the first frame update
    void Start()
    {
        initStart(false);
        newDialogLine("Ohh god, that's a big tree guy!", 1);
        newDialogLine("Be carefull!", 3);
        newDialogLine("Remember, you might have something usefull in your backpack.", 3);
    }

    // Update is called once per frame
    void Update()
    {
        initUpdate();
    }
}
