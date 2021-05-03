using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gargoyle_dialog : Dialog
{
    // Start is called before the first frame update
    void Start()
    {
        initStart(false);
        newDialogLine("What a creepy statue...", 1);
        newDialogLine("It's actually a Gargoyle.", 3);
        newDialogLine("Wow, I wonder what it is doing here?", 0);
        newDialogLine("Who dares to enter my domain?", 4);
        newDialogLine("Ahhh! It talks!", 1); 
        newDialogLine("Who are you?", 4);
        newDialogLine("It is I, Dio!!!", 2);
        newDialogLine("Tell me, what do you want?", 4);
        newDialogLine("I'm looking for my brother.", 0);
        newDialogLine("He's a bit shorter than me and has brown hair.", 0);
        newDialogLine("Have you seen him?", 0);
        newDialogLine("I have not seen anyone with that description", 4);
        newDialogLine("But if he is in this place...", 4);
        newDialogLine("... he is in grave danger!", 4);
        newDialogLine("H-how come?", 1);
        newDialogLine("This place is filled with dark tree spirits!", 4);
        newDialogLine("Tread carefully.", 4);

    }
    // Update is called once per frame
    void Update()
    {
        dialog();
    }
}
