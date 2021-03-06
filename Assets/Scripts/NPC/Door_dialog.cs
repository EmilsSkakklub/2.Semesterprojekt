using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_dialog : Dialog
{
    // Start is called before the first frame update
    void Start()
    {
        initStart();
        newDialogLine("Hello Door, how are you doing?");
        newDialogLine("Why are you now answering me?");
        newDialogLine("Well, maybe I should not exit here anyway...");

    }

    // Update is called once per frame
    void Update()
    {
        dialog();
    }

    
}
