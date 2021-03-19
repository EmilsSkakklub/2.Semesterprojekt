using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fridge_dialog : Dialog
{
    // Start is called before the first frame update
    void Awake()
    {
        initStart(false);
        newDialogLine("I'm not feeling hungry right now.", 0);
        newDialogLine("But maybe I will come back later.", 0);
    }

    // Update is called once per frame
    void Update()
    {
        dialog();
    }
}
