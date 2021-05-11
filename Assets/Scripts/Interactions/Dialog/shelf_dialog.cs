using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shelf_dialog : Dialog
{
    // Start is called before the first frame update
    void Start()
    {
        initStart(false);
        newDialogLine("Nothing usefull here", 0);
    }

    // Update is called once per frame
    void Update()
    {
        dialog();
    }
}
