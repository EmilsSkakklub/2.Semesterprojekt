using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_1_Load : LevelChanger
{
    // Start is called before the first frame update
    void Start()
    {
        InitStart("L1S1");
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(ChangeLevel());
    }
}
