using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    GameObject playLine;
    GameObject resumeLine;
    GameObject quitLine;
    GameObject Play;
    GameObject Resume;
    GameObject Quit;

    GameObject MainStart;
    GameObject MainResume;

    // Start is called before the first frame update
    private void Awake() {
        Play = GameObject.Find("Play");
        Resume = GameObject.Find("Resume");
        Quit = GameObject.Find("Quit");

        MainStart = GameObject.Find("Main_start");
        MainResume = GameObject.Find("Main_resume");

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    void Start()
    {
        MainResume.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (MainStart.activeInHierarchy) {
            Resume.SetActive(false);
            Play.SetActive(true);
            Quit.SetActive(true);
        }else if (MainResume.activeInHierarchy) {
            Resume.SetActive(true);
            Play.SetActive(false);
            Quit.SetActive(true);
        }
    }
}
