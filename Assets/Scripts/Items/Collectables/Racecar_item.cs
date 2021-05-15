using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racecar_item : Item
{
   

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
    
    public override void useItem() {
        audioManager.Play("UIClick", false, 0.1f, 1);
    }
}
