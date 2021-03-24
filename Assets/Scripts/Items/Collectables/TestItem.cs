using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItem : Item
{
    // Start is called before the first frame update
    void Start()
    {
        initStart();
    }

    // Update is called once per frame
    void Update()
    {
        initUpdate("Test Item");
    }
}
