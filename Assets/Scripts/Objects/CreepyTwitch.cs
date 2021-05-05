using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepyTwitch : MonoBehaviour
{

    public bool startTwitching;
    public int randomNum;
    private float frequency = 20f;
    private float amplitude = 0.05f;
    private Vector3 posOffset = new Vector3();
    private Vector3 tempPos = new Vector3();


    // Start is called before the first frame update
    void Start()
    {
        posOffset = transform.position;
        InvokeRepeating("GetRandomNumber", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        Twitch();
    }


    private void Twitch() {
       
        if(randomNum <= 2) {
            startTwitching = true;
        }

        if (startTwitching) {
            tempPos = posOffset;
            tempPos.x += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;
            transform.position = tempPos;
            Invoke("GetIsHitFalse", 0.2f);
        }
        
    }

    private void GetIsHitFalse() {
        startTwitching = false;
    }

    private void GetRandomNumber() {
        randomNum = Random.Range(0, 15);
    }
}
