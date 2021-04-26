using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatBush : MonoBehaviour
{

    public int health;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("Bush");
        health = 3;
    }

    public void takeDamage() {
        health--;
    }





}
