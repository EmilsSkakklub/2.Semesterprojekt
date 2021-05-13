using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxL1N1 : BoxDialog
{
    // Start is called before the first frame update
    void Start()
    {
        initStart(false);
        newDialogLine("Wow, that's a big maze.", 3);
        newDialogLine("Wait... you talk Teddy?", 0);
        newDialogLine("We don't have time, we need to hurry!", 3);
    }

    // Update is called once per frame
    void Update()
    {
        initUpdate();
        ChangeStoryNumber(10);
    }
}
