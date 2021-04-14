using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_0_Load : LevelChanger
{
    // Start is called before the first frame update
    void Start()
    {
        InitStart("L0S2");   
    }

    // Update is called once per frame
    void Update()
    {
        ChangeLevel();
    }
}
