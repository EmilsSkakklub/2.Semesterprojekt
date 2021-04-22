using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lvl1HoleInteract : Dialog
{
    // Start is called before the first frame update
    void Awake()
    {
        initStart(false);
        newDialogLine("I can't go back now. I must find my brother", 0);
    }

    // Update is called once per frame
    void Update()
    {
        dialog();
    }
}
