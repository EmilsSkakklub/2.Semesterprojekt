using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crath_Dialog : Dialog
{
    // Start is called before the first frame update
    void Start()
    {
        initStart(false);
        newDialogLine("That's one big bush!", 0);
        newDialogLine("I wonder whats behind it", 1);
        newDialogLine("Maybe I can cut it down with something...", 0);

    }

    // Update is called once per frame
    void Update()
    {
        dialog();
    }
}
