using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxL2N2 : BoxDialog
{
    // Start is called before the first frame update
    void Start()
    {
        initStart(false);
        newDialogLine("These walking trees sure are scary!", 1);
        newDialogLine("Fear not, for I am by your side!", 3);
        newDialogLine("Thanks Teddy, I can always count on you", 0);
    }

    // Update is called once per frame
    void Update()
    {
        initUpdate();
    }
}
