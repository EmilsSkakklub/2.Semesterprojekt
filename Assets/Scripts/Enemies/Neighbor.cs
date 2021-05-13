using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neighbor : Enemy
{
    public ObjectiveChanger objectiveChanger;

    // Start is called before the first frame update
    void Start()
    {
        objectiveChanger = GameObject.Find("GameManager").GetComponent<ObjectiveChanger>();

        initStart("Neighbor", 2, 200, 3f, 1.5f, 20f, "HitWood");
    }

    // Update is called once per frame
    void Update()
    {
        initUpdate();


        if (isDead) {
            Invoke("Objective18", 4);
        }
    }


    private void Objective18() {
        objectiveChanger.SetStoryNumber(18);
    }
}
