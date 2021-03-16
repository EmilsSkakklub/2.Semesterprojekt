using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpirit : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        initStart("TreeSpirit", 1, 10, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        initUpdate();
        
    }
}
