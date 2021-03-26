using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple_Item : Item
{
    // Start is called before the first frame update
    void Start()
    {
        initStart("Apple", true);
    }

    // Update is called once per frame
    void Update()
    {
        initUpdate();
    }
    public override void useItem() {
        print("Apple, hey");
    }

}
