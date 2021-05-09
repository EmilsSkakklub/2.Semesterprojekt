using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxL1N3 : BoxDialog
{
    // Start is called before the first frame update
    void Start()
    {
        initStart(false);
        newDialogLine("Hmm, seems like the road splits in two.", 3);
    }

    // Update is called once per frame
    void Update()
    {
        initUpdate();
        SetNewSpawnPoint(transform);
    }
}
