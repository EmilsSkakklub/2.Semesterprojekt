using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastBushHole_dialog : Dialog
{
    private LittleBroScript bro;

    // Start is called before the first frame update
    void Start()
    {
        bro = GameObject.Find("LittleBro2").GetComponent<LittleBroScript>();
        initStart(false);
        newDialogLine("I should probably get my broter first", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!bro.followPlayer) {
            dialog();
        }
    }
}
