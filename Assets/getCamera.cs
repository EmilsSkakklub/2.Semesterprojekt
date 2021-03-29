using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getCamera : MonoBehaviour
{
    Camera maincam;
    Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
        maincam = GameObject.Find("Main Camera").GetComponent<Camera>();
        canvas.worldCamera = maincam;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
