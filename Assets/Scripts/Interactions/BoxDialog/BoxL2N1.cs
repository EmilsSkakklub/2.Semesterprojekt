using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxL2N1 : BoxDialog
{
    // Start is called before the first frame update
    void Start()
    {
        initStart(false);
        newDialogLine("What is this place?", 2);
        newDialogLine("Is this really Mr. Dargons backyard?", 2);
        newDialogLine("We can't stop now, we need to keep on going!", 3);
    }

    // Update is called once per frame
    void Update()
    {

        initUpdate();
    }
}
