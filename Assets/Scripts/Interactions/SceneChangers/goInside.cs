using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goInside : SceneChange
{
    void Start() {
        initStart();
        SceneChangeTo("LevelHouse");
    }


    void Update() {
        StartCoroutine(SceneChanger());
    }
}
