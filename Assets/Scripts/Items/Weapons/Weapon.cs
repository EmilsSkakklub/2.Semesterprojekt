using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public int damage;
    public bool isEquiped;

    private PlayerScript playerScript;

    protected void InitStart(int damage) {
        setDamage(damage);
        playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
        gameObject.tag = "Weapon";

        setIsEquiped(false);
        gameObject.SetActive(false);
    }


    public void equip() {
        gameObject.SetActive(true);
        playerScript.setEquipedWeapon(this);
    }

    public void dequip() {
        gameObject.SetActive(false);
        playerScript.setEquipedWeapon(null);
    }



    //getters and setters
    public int getDamage() {
        return damage;
    }
    public void setDamage(int damage) {
        this.damage = damage;
    }

    public bool getIsEquiped() {
        return isEquiped;
    }
    public void setIsEquiped(bool isEquiped) {
        this.isEquiped = isEquiped;
    }

    public PlayerScript getplayerScript() {
        return playerScript;
    }


}
