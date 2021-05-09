using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_0_Loud2 : LevelChanger
{
    // Start is called before the first frame update
    void Start()
    {
        InitStart("L0S3");
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(ChangeLevel(2f));
        PlayerSound("OpenDoor");
    }
}
