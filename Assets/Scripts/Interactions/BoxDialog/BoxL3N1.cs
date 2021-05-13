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
        newDialogLine("Arthur, I sense that this place is filled with some of our old toys.", 3);
        newDialogLine("We should find them all, this might help us find him.", 3);
        newDialogLine("Hmm, you're ´probably right teddy.", 0);
    }

    // Update is called once per frame
    void Update()
    {
        initUpdate();
        
    }
}
