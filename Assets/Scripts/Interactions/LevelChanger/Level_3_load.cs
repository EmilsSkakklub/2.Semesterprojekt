using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_3_load : LevelChanger
{
    // Start is called before the first frame update
    void Start()
    {
        InitStart("L3S1");
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(ChangeLevel(0));
        ChangeStoryNumber(12);
    }


}
