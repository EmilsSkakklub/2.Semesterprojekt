using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class littleTeddy_Item : Item
{
    public override void useItem() {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        initStart("Little Brothers Teddy", false, false, "My little Brothers Teddy.");
    }

    // Update is called once per frame
    void Update()
    {
        initUpdate();
    }
}
