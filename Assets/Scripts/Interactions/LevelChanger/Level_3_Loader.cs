using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_3_Loader : LevelChanger
{
    // Start is called before the first frame update
    void Start()
    {
        InitStart("L3S1");
    }

    // Update is called once per frame
    void Update()
    {
        if (interaction.getStartInteraction()) {
            StartCoroutine(ChangeLevel(0.1f));
            ChangeStoryNumber(12);
        }
    }


    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            interaction.setStartInteraction(true);
        }
    }
}
