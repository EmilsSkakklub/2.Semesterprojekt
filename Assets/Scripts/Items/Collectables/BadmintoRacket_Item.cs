using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadmintoRacket_Item : Item
{
   

    // Start is called before the first frame update
    void Start()
    {
        initStart("Racket", false, false, "Permanently increases attack damage by +1");
    }

    // Update is called once per frame
    void Update()
    {
        initUpdate();
    } 
    
    public override void useItem() {
        audioManager.Play("UIClick", false, 0.1f, 1);
    }
}
