using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Home_Load : LevelChanger
{

    private LittleBroScript bro;

    // Start is called before the first frame update
    void Start()
    {
        bro = GameObject.Find("LittleBro2").GetComponent<LittleBroScript>();
        InitStart("L4S2");
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(ChangeLevel(0.1f));

        if (interaction.getStartInteraction()) {
            objectiveChanger.SetStoryNumber(14);
        }
    }
}
