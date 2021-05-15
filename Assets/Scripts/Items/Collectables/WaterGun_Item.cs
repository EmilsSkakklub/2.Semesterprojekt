using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGun_Item : Item
{

    // Start is called before the first frame update
    void Start()
    {
        initStart("Water Gun", false, false, "Permanently increases stamina.");
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
