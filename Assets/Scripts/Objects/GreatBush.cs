using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatBush : MonoBehaviour
{

    private int health;
    private float frequency = 15f;
    private float amplitude = 0.05f;
    private bool isHit;
    private Vector3 posOffset = new Vector3();
    private Vector3 tempPos = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("Bush");
        posOffset = transform.position;
        health = 3;
    }

    public void takeDamage() {
        health--;
        isHit = true;
    }

    public void shakeBush() {
        if(isHit == true) {
            tempPos = posOffset;
            tempPos.x += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;
            transform.position = tempPos;
            Invoke("setIsHitFalse", 0.3f);
        }
    }

    public void setIsHitFalse() {
        isHit = false;
    }

    public void destroyTheBush() {
        if(health <= 0) {
            gameObject.SetActive(false);
        }
    }

    private void Update() {
        destroyTheBush();
        shakeBush();
    }



}
