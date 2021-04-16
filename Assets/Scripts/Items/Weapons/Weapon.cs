using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    private int damage;
    private PlayerScript playerScript;
    private CapsuleCollider hitBox;



    protected void Init(int damage) {
        setDamage(damage);
        playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
        hitBox = GetComponent<CapsuleCollider>();

        hitBox.isTrigger = true;
    }





    //getters and setters
    public int getDamage() {
        return damage;
    }

    public void setDamage(int damage) {
        this.damage = damage;
    }


    public PlayerScript getplayerScript() {
        return playerScript;
    }


    public CapsuleCollider getHitBox() {
        return hitBox;
    }

}
