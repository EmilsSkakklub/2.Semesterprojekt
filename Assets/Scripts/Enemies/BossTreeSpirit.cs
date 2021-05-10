using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTreeSpirit : Enemy
{
    public Item theKey;

    // Start is called before the first frame update
    void Start()
    {
        theKey = GameObject.Find("key").GetComponent<Item>();
        initStart("BossTree",2, 100, 2f, 1.5f, 10f, "HitWood");
    }

    // Update is called once per frame
    void Update()
    {
        initUpdate();
        if (!isDead) {
            theKey.gameObject.SetActive(false);
        }
        else if (isDead) {
            Invoke("SpawnKey", 3);
        }
    }


    private void SpawnKey() {
        theKey.gameObject.SetActive(true);
    }

}
