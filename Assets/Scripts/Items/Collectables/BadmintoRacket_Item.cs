using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadmintoRacket_Item : Item
{
    public override void useItem() {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        initStart("Racket", false, false, "I haven't played badminton for a while.");
    }

    // Update is called once per frame
    void Update()
    {
        initUpdate();
    }
}
