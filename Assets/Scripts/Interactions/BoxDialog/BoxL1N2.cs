using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxL1N2 : BoxDialog
{
    // Start is called before the first frame update
    void Start()
    {
        initStart(false);
        newDialogLine("Impressive jumping!", 3);
    }

    // Update is called once per frame
    void Update()
    {
        initUpdate();
    }
}
