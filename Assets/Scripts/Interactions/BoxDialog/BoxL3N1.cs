using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxL3N1 : BoxDialog
{
    // Start is called before the first frame update
    void Start()
    {
        initStart(false);
        newDialogLine("This place really creeps me out.", 1);
        newDialogLine("He must be around here somewhere!", 3);
        newDialogLine("Be brave!", 3);
        newDialogLine("I'll try I guess...", 1);
    }

    // Update is called once per frame
    void Update()
    {
        initUpdate();
    }
}
