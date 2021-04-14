using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_House_Loud : LevelChanger
{
    // Start is called before the first frame update
    void Start()
    {
        InitStart("LHS1");
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(ChangeLevel());
    }
}
