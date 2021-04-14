using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelChanger : MonoBehaviour
{
    private PlayerScript playerScript;
    private Interaction interaction;
    private Transform newSpawnPoint;


    protected void InitStart(string spawnPointName) {
        playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
        interaction = GetComponent<Interaction>();
        newSpawnPoint = GameObject.Find(spawnPointName).GetComponent<Transform>();
    }

    protected void ChangeLevel() {
        if (interaction.getStartInteraction()) {
            playerScript.transform.position = new Vector3(newSpawnPoint.position.x, newSpawnPoint.position.y, newSpawnPoint.position.z);
            playerScript.transform.transform.Rotate(newSpawnPoint.eulerAngles.x, newSpawnPoint.eulerAngles.y, newSpawnPoint.eulerAngles.z);
            Invoke("ResetStartInteract", 0.2f);
        }
    }

    private void ResetStartInteract() {
        interaction.setStartInteraction(false);
    }

}
