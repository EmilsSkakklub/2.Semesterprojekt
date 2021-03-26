using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItem : Item
{

    // Start is called before the first frame update
    void Start()
    {
        initStart("Test Item", false);
    }

    // Update is called once per frame
    void Update()
    {
        initUpdate();
    }    
    
    //what to do when using item
    public override void useItem() {
        print("You used " + getName());
    }
}
