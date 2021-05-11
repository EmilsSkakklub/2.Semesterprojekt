using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGun_Item : Item
{
    public override void useItem() {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        initStart("Water Gun", false, false, "My old water gun. There's still water in it! Pew Pew.");
    }

    // Update is called once per frame
    void Update()
    {
        initUpdate();   
    }
}
