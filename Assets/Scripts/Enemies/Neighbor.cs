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

        initStart("Neighbor", 2, 200, 3f, 1.5f, 10f, "NeighborHit");
        audioManager.GetSound("NeighborHit").volume = 0.2f;
        audioManager.GetSound("NeighborHit").pitch = Random.Range(0.5f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        initUpdate();
        CheckDeath();
    }

    private void CheckDeath() {
        if (isDead) {
            Invoke("Objective13", 4);
        }
        if (ps.getIsDead()) {
            health = 200;
        }
    }


    private void Objective13() {
        objectiveChanger.SetStoryNumber(13);
    }
}
