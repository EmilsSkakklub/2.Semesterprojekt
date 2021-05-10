using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple_Item : Item
{

    
    // Start is called before the first frame update
    void Start()
    {
        
        initStart("Apple", true, false, "Consume to recover 2 full hearts");
    }

    // Update is called once per frame
    void Update()
    {
        initUpdate();
    }
    public override void useItem() {
        player.heal(4);
        audioManager.Play("AppleSound", false, 0.5f, 1);
    }

}
