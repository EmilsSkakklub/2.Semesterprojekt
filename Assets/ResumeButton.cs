using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResumeButton : MonoBehaviour {
    GameObject resumeLine;

    GameObject MainStart;
    GameObject MainResume;

    // Start is called before the first frame update
    void Awake() {
        resumeLine = GameObject.Find("ResumeLine");

        MainStart = GameObject.Find("Main_start");
        MainResume = GameObject.Find("Main_resume");
    }
    private void Start() {
        resumeLine.SetActive(false);
    }

    private void OnMouseOver() {
        if (MainResume.activeInHierarchy) {
            resumeLine.SetActive(true);
        }

    }
    private void OnMouseExit() {
        if (MainResume.activeInHierarchy) {
            resumeLine.SetActive(false);
        }

    }

}
