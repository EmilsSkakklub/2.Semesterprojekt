using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class setToHead : MonoBehaviour
{

    public CinemachineFreeLook vcam;
    Transform head;

    // Start is called before the first frame update
    void Start()
    {
        head = GameObject.Find("Head").GetComponent<Transform>();
        vcam = GetComponent<CinemachineFreeLook>();
        vcam.Follow = head;
        vcam.LookAt = head;
    }
}
