using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpirit : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        initStart("TreeSpirit", 10, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        initUpdate();
        
    }
}
