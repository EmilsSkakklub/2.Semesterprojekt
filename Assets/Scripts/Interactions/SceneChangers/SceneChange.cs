using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{

    Interaction interaction;
    Animator transition;
    float transitionTime = 1f;
    string sceneName;


    protected void initStart() {
        transition = GameObject.Find("Crossfade").GetComponent<Animator>();
        interaction = GetComponent<Interaction>();
    }
    protected void SceneChangeTo(string sceneName) {
        this.sceneName = sceneName;
        
    }

    protected IEnumerator SceneChanger() {
        if (interaction.getStartInteraction()) {
            transition.SetTrigger("Start");
            yield return new WaitForSeconds(transitionTime);
            SceneManager.LoadScene(sceneName);
        }
    }
}
