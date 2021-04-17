using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenSword_Item : Item
{
    public Weapon weapon;

    // Start is called before the first frame update
    void Awake()
    {
        initStart("Wooden Sword",false, "My old Wooden Sword, Cool!");
        weapon = GameObject.Find("woodenSword_weapon").GetComponent<Weapon>();
    }

    // Update is called once per frame
    void Update()
    {
        initUpdate();
    }


    public override void useItem() {
        weapon.equip();
        player.getInventory().getWeaponSlot().setItem(this);
    }
}
