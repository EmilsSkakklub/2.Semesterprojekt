using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teddy_Item : Item
{
    

    // Start is called before the first frame update
    void Start()
    {
        initStart("Teddy", false, false, "My best friend Teddy");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public override void useItem() {
        print("Hello there, I am Teddy!");
        audioManager.Play("Squeak", false, 0.2f, 1f);
    }
}
