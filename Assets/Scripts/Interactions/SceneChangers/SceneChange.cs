using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    PlayerScript ps;
    Interaction interaction;
    Animator transition;
    float transitionTime = 1f;
    string sceneName;


    protected void initStart() {
        transition = GameObject.Find("Crossfade").GetComponent<Animator>();
        ps = GameObject.Find("Player").GetComponent<PlayerScript>();
        interaction = GetComponent<Interaction>();
    }
    protected void SceneChangeTo(string sceneName) {
        this.sceneName = sceneName;
        
    }

    protected IEnumerator SceneChanger() {
        if (interaction.getStartInteraction()) {
            ps.ListOfInteractables.Clear();
            transition.SetTrigger("Start");
            yield return new WaitForSeconds(transitionTime);
            SceneManager.LoadScene(sceneName);
        }
    }
}
