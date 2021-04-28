using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTreeSpirit : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        initStart("BossTree",2, 100, 2f, 1.5f, 10f);
        
    }

    // Update is called once per frame
    void Update()
    {
        initUpdate();

    }
}
