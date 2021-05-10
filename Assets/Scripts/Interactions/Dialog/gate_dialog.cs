using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gate_dialog : Dialog
{
    // Start is called before the first frame update
    void Start()
    {
        initStart(false);
        newDialogLine("The Gate is locked...", 1);
    }

    // Update is called once per frame
    void Update()
    {
        dialog();
    }
}
