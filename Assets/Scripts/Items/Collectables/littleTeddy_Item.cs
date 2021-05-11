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
        initStart("Cuddles", false, false, "My little brother's teddy bear.");
    }

    // Update is called once per frame
    void Update()
    {
        initUpdate();
    }
}
