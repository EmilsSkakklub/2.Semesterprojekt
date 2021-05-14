using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racecar_item : Item
{
    public override void useItem() {
        print("Toy Car");
    }

    // Start is called before the first frame update
    void Start()
    {
        initStart("Toy Car", false, false, "Permanently increases movement speed while running.");   
    }

    // Update is called once per frame
    void Update()
    {
        initUpdate();
    }
}
