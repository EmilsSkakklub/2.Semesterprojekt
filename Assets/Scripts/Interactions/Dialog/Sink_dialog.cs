using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sink_dialog : Dialog
{
    // Start is called before the first frame update
    void Awake()
    {
        initStart(false);
        newDialogLine("My dad always told me to remember to wash my hands,", 0);
        newDialogLine(" whenever i had been outside.", 0);
    }

    // Update is called once per frame
    void Update()
    {
        dialog();
    }
}
