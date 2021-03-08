using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    private int health;
    private string enemyName;

    protected void Init(string enemyName, int health) {
        setEnemyName(enemyName);
        setHealth(health);
    }

    public void takeDamage(int damage) {
       health -= damage;
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
