using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelChanger : MonoBehaviour
{
    private PlayerScript playerScript;
    private Interaction interaction;
    private Transform newSpawnPoint;
    private Animator transition;

    private float transitionTime = 1f;


    protected void InitStart(string spawnPointName) {
        playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
        transition = GameObject.Find("Crossfade").GetComponent<Animator>();
        interaction = GetComponent<Interaction>();
        newSpawnPoint = GameObject.Find(spawnPointName).GetComponent<Transform>();
    }

    protected IEnumerator ChangeLevel() {
        if (interaction.getStartInteraction()) {
            transition.SetBool("Start", true);
            
            yield return new WaitForSeconds(transitionTime);
            transition.SetBool("Start", false);
            playerScript.transform.position = new Vector3(newSpawnPoint.position.x, newSpawnPoint.position.y, newSpawnPoint.position.z);
            playerScript.transform.transform.Rotate(newSpawnPoint.eulerAngles.x, newSpawnPoint.eulerAngles.y, newSpawnPoint.eulerAngles.z);
            Invoke("ResetStartInteract", 0.1f);

            
        }
    }

    private void ResetStartInteract() {
        interaction.setStartInteraction(false);
    }

}
