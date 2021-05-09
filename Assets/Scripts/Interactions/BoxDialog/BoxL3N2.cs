using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxL3N2 : BoxDialog
{
    // Start is called before the first frame update
    void Start()
    {
        initStart(false);
        newDialogLine("Man, I really hate gnomes", 1);

    }

    // Update is called once per frame
    void Update()
    {
        initUpdate();
    }
}
