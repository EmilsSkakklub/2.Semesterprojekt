using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseKeyAtBigDoor : MonoBehaviour
{
    public bool isIntheBox;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            isIntheBox = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            isIntheBox = false;
        }
    }
}
