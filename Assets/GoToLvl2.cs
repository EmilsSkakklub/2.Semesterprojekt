using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToLvl2 : LevelChanger
{
    // Start is called before the first frame update
    void Start() {
        InitStart("L2S1");
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(ChangeLevel());
    }
}
