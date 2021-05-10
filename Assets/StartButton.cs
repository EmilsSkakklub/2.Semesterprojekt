using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour {
    GameObject playLine;

    GameObject MainStart;
    GameObject MainResume;

    // Start is called before the first frame update
    void Awake() {
        playLine = GameObject.Find("PlayLine");

        MainStart = GameObject.Find("Main_start");
        MainResume = GameObject.Find("Main_resume");
    }
    private void Start() {
        playLine.SetActive(false);
    }

    private void OnMouseOver() {
        if (MainStart.activeInHierarchy) {
            playLine.SetActive(true);
            if (Input.GetMouseButtonDown(0)) {
                Debug.Log("StartGame");
                SceneManager.LoadScene(1);
            }
        }
            
    }
    private void OnMouseExit() {
        if (MainStart.activeInHierarchy) {
            playLine.SetActive(false);
        }
        
    }

}
