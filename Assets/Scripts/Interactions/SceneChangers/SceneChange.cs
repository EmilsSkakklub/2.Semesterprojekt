using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{

    Interaction interaction;
    string sceneName;


    protected void initStart() {
        interaction = GetComponent<Interaction>();
    }
    protected void SceneChangeTo(string sceneName) {
        this.sceneName = sceneName;
    }

    protected void SceneChanger() {
        if (interaction.getStartInteraction()) {
            SceneManager.LoadScene(sceneName);
        }
    }
}
