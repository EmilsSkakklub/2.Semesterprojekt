using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtBall : MonoBehaviour
{
    Transform football;
    Transform cam;
    // Start is called before the first frame update
    void Start()
    {
        football = GameObject.Find("Football").GetComponent<Transform>();
        cam = GameObject.Find("FootballCamera").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        cam.transform.LookAt(football.position);
    }
}
