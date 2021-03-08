using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goOutside : SceneChange
{
    void Start()
    {
        initStart();
        SceneChangeTo("Level0");
    }


    void Update()
    {
        StartCoroutine(SceneChanger());
    }
}
