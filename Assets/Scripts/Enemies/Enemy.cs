using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int health;
    public string enemyName;

    protected void initStart(string enemyName, int health) {
        gameObject.layer = LayerMask.NameToLayer("Enemy");
        setEnemyName(enemyName);
        setHealth(health);
    }

    protected void initUpdate() {
        die();
    }


    public void takeDamage(int damage) {
       health -= damage;
    }

    public void die() {
        if(health <= 0) {
            Destroy(gameObject);
        }
    }





    //getters and setters
    public int getHealth() {
        return health;
    }

    public void setHealth(int health) {
        this.health = health;
    }

    public string getEnemyName() {
        return enemyName;
    }

    public void setEnemyName(string enemyName) {
        this.enemyName = enemyName;
    }

    
}
