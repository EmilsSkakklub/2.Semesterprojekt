using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_dialog : Dialog
{

    void Awake()
    {
        initStart();
        newDialogLine("Hello Door, how are you doing?"); moodSprite(0); //0=normal 1=sad 2=angry
        newDialogLine("Why are you not answering me?"); moodSprite(1); //0=normal 1=sad 2=angry
        newDialogLine("Well, maybe I should not exit here anyway..."); moodSprite(1); //0=normal 1=sad 2=angry

    }

    // Update is called once per frame
    void Update()
    {
        dialog();
    }

    
}
