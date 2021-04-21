using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    PlayerScript ps;
    Interaction interaction;
    Animator transition;
    GameManager gameManager;
    float transitionTime = 1f;
    string sceneName;


    protected void initStart() {
        transition = GameObject.Find("Crossfade").GetComponent<Animator>();
        ps = GameObject.Find("Player").GetComponent<PlayerScript>();
        interaction = GetComponent<Interaction>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    protected void SceneChangeTo(string sceneName) {
        this.sceneName = sceneName;
        gameManager.clearEnemyList();

        //ps.LoadPlayer();
    }

    protected IEnumerator SceneChanger(string spawnpoint) {
        if (interaction.getStartInteraction()) {
            ps.spawnPointName = spawnpoint;
            ps.ListOfInteractables.Clear();
            transition.SetTrigger("Start");

            yield return new WaitForSeconds(transitionTime);
            //ps.savePlayer();

            SceneManager.LoadScene(sceneName);
        }
    }
}
