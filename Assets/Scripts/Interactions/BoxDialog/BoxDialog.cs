using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BoxDialog : Dialog
{

    public bool doOnce;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            interaction.setStartInteraction(true);
            doOnce = true;
        }
    }

    protected void initUpdate() {
        dialog();
        if (doOnce && !interaction.getStartInteraction()) {
            gameObject.SetActive(false);
        }
    }


    protected void SetNewSpawnPoint(Transform transform) {
        if (interaction.getStartInteraction()) {
            GetPlayerScript().setSpawnPoint(transform);
        }
    }
}
