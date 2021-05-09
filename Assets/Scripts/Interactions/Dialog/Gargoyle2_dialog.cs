using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gargoyle2_dialog : Dialog
{
    // Start is called before the first frame update
    void Start()
    {
        initStart(false);
        newDialogLine("Yo, what's up?", 0);
        newDialogLine("...", 3);
        newDialogLine("Seems like this one doesn't want to talk.", 1);
    }

    // Update is called once per frame
    void Update()
    {
        dialog();
    }
}
