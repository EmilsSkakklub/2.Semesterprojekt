using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree_Dialog : Dialog
{
    // Start is called before the first frame update
    void Start()
    {
        initStart();
        newDialogLine("I have always wanted to climb to the top!"); moodSprite(0); //0=normal 1=sad 2=angry
    }

    // Update is called once per frame
    void Update()
    {
        dialog();
    }
}
