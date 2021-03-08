using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goToLvl1 : SceneChange {
    void Start() {
        initStart();
        SceneChangeTo("Level1");
    }


    void Update() {
        StartCoroutine(SceneChanger());
    }
}
