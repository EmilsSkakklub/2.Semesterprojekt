using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxChest : BoxDialog
{

    // Start is called before the first frame update
    void Start()
    {
        initStart(false);
        newDialogLine("Cool, a chest! I wonder whats inside.", 0);
    }

    // Update is called once per frame
    void Update()
    {
        initUpdate();
    }
}
