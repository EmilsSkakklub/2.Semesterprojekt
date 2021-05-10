using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallTreeSpirit : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        initStart("SmallTreeSpirit", 1, 10, 2f, 0.6f, 5f, "HitWood");
    }

    // Update is called once per frame
    void Update()
    {
        initUpdate();
    }
}
