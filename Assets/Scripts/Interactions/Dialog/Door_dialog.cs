using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_dialog : Dialog
{

    void Awake()
    {
        initStart();
        newDialogLine("Knock, knock...               '",0);//0=normal 1=sad 2=angry
        newDialogLine("Why are you not answering me?",1);  //0=normal 1=sad 2=angry
        newDialogLine("Well, maybe I should not talk to a door anyway...",1);  //0=normal 1=sad 2=angry

    }

    // Update is called once per frame
    void Update()
    {
        dialog();
    }

    
}
