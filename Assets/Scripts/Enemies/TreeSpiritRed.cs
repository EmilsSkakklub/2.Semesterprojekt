using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpiritRed : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        initStart("Red Tree Spirit",1, 30, 2.5f, 0.8f, 6f, "HitWood");
    }

    // Update is called once per frame
    void Update()
    {
        initUpdate();
    }
}
