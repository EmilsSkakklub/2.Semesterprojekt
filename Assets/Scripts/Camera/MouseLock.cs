using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLock : MonoBehaviour
{

    public Transform playerBody;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GameObject.Find("ProtoPlayer").GetComponent<Transform>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
