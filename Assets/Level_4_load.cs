using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_4_load : LevelChanger
{
    private Enemy neighbor;
    private bool doOnce = false;

    // Start is called before the first frame update
    void Start() {
        neighbor = GameObject.Find("Neighbor").GetComponent<Enemy>();
        InitStart("L4S1");
    }

    // Update is called once per frame
    void Update() {
        StartCoroutine(ChangeLevel(0));

        if (neighbor.isDead && !doOnce) {
            Invoke("LoadNextScene", 3);
            doOnce = true;
        }
    }

    private void LoadNextScene() {
        interaction.setStartInteraction(true);
    }
}
