using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tallGrass : MonoBehaviour
{

    private PlayerScript player;
    private BoxCollider boxCollider;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
        boxCollider = GetComponent<BoxCollider>();

        boxCollider.isTrigger = true;
    }


    private void OnTriggerStay(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            if (player.getCrouchToggle() && !player.getIsInCombat()) {
                player.setIsStealth(true);
            }
            else {
                player.setIsStealth(false);
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            player.setIsStealth(false);
        }
    }

}
