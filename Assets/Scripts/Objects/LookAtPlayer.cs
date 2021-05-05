using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private Transform playerTransform;
    

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        lookAtPlayer();
    }

    private void lookAtPlayer() {
        Vector3 lookPos = playerTransform.position - transform.position;
        lookPos.y = 0;
        Quaternion rotate = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotate, Time.deltaTime * 5);
    }
}
