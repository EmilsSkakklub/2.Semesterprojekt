using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neighbor : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        initStart("Neighbor", 2, 200, 3f, 1.5f, 20f, "HitWood");
    }

    // Update is called once per frame
    void Update()
    {
        initUpdate();
    }
}
