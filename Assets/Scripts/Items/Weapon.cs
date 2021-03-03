using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    private int damage;
    private string weaponTag;
    private PlayerScript playerScript;
    private CapsuleCollider hitBox;
    


    protected void Init(int damage, string weaponTag) {
        setDamage(damage);
        setWeaponTag(weaponTag);
        setPlayerScript();
        setHitBox();

        hitBox.isTrigger = true;
        hitBox.enabled = false;
    }


    protected void attackCheck() {
        if (getplayerScript().getAnimator().GetCurrentAnimatorStateInfo(0).IsTag("1") ||
            getplayerScript().getAnimator().GetCurrentAnimatorStateInfo(0).IsTag("2") ||
            getplayerScript().getAnimator().GetCurrentAnimatorStateInfo(0).IsTag("3") ||
            getplayerScript().getAnimator().GetCurrentAnimatorStateInfo(0).IsTag("4")) {
            hitBox.enabled = true;
        }
        else {
            hitBox.enabled = false;
        }
    }



    //getters and setters
    public int getDamage() {
        return damage;
    }

    public void setDamage(int damage) {
        this.damage = damage;
    }

    public string getWeaponTag() {
        return weaponTag;
    }

    public void setWeaponTag(string weaponTag) {
        this.weaponTag = weaponTag;
        gameObject.tag = weaponTag;
    }


    public PlayerScript getplayerScript() {
        return playerScript;
    }

    public void setPlayerScript() {
        playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
        
    }

    public CapsuleCollider getHitBox() {
        return hitBox;
    }

    public void setHitBox() {
        hitBox = GetComponent<CapsuleCollider>();
    }

}
