using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick_Item : Item
{
    public Weapon weapon;


    private void Awake() {
        initStart("Stick", false, true, "This is a stick, Meh! Damage increase: +1");
        weapon = GameObject.Find("Stick_Weapon").GetComponent<Weapon>();
    }

    // Update is called once per frame
    void Update()
    {
        initUpdate();
    }

    public override void useItem() {
        if (player.getEquipedWeapon() == null) {
            weapon.equip();
            player.getInventory().getWeaponSlot().setItem(this);
        }
        else if (player.getEquipedWeapon() == weapon) {
            weapon.dequip();
            player.getInventory().getWeaponSlot().setItem(null);
        }
    }
}

