using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnegyBottle_Item : Item
{


    // Start is called before the first frame update
    void Start()
    {
        initStart("Peak Dew", true, false, "Consume to gain a +2 damage boost for 60 seconds");
    }

    // Update is called once per frame
    void Update()
    {
        initUpdate();
    }

    public override void useItem() {
        player.EnergyBuff();
    }
}
