using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour {
    GameObject quitLine;

    GameObject MainStart;
    GameObject MainResume;

    // Start is called before the first frame update
    void Awake() {
        quitLine = GameObject.Find("QuitLine");

        MainStart = GameObject.Find("Main_start");
        MainResume = GameObject.Find("Main_resume");
    }
    private void Start() {
        quitLine.SetActive(false);
    }

    private void OnMouseOver() {
        if (MainResume.activeInHierarchy || MainStart.activeInHierarchy) {
            quitLine.SetActive(true);
            if (Input.GetMouseButtonDown(0)) {
                Debug.Log("Quit");
                Application.Quit();
            }
        }

    }
    private void OnMouseExit() {
        if (MainResume.activeInHierarchy || MainStart.activeInHierarchy) {
            quitLine.SetActive(false);
        }

    }

}
