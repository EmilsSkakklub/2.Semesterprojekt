using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpirit : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        initStart("TreeSpirit", 20);
    }

    // Update is called once per frame
    void Update()
    {
        initUpdate();
    }
}
