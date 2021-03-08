using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHUNT : MonoBehaviour
{

    private Transform playerTransform;
    private float moveSpeed = 1f;
    private float MaxDistance = 1f;
    private float MinDistance = 1f;



    private void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }


    // Update is called once per frame
    void Update()
    {
        transform.LookAt(playerTransform);

        if(Vector3.Distance(transform.position, playerTransform.position) >= MinDistance)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;

            if(Vector3.Distance(transform.position, playerTransform.position) <= MaxDistance)
            {
                //implement Attack
            }
        }
    }




}
