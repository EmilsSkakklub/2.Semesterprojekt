using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisableOnWrongScene : MonoBehaviour
{
    GameObject LH;
    GameObject L0;
    GameObject L1;
    GameObject L2;
    GameObject L3;
    GameObject L4;

    // Start is called before the first frame update
    void Start()
    {
        LH = GameObject.Find("LevelHouse");
        L0 = GameObject.Find("Level0");
        L1 = GameObject.Find("Level1");
        L2 = GameObject.Find("Level2");
        L3 = GameObject.Find("Level3");
        L4 = GameObject.Find("Level4");
    }

    // Update is called once per frame
    void Update()
    {
        switch (SceneManager.GetActiveScene().name) {
        case "LevelHouse":
            LH.SetActive(true);
            L0.SetActive(false);
            L1.SetActive(false);
            L2.SetActive(false);
            L3.SetActive(false);
            L4.SetActive(false);
            break;
        case "Level0":
            LH.SetActive(false);
            L0.SetActive(true);
            L1.SetActive(false);
            L2.SetActive(false);
            L3.SetActive(false);
            L4.SetActive(false);
            break;
        case "Level1":
            LH.SetActive(false);
            L0.SetActive(false);
            L1.SetActive(true);
            L2.SetActive(false);
            L3.SetActive(false);
            L4.SetActive(false);
            break;
        case "Level2":
            LH.SetActive(false);
            L0.SetActive(false);
            L1.SetActive(false);
            L2.SetActive(true);
            L3.SetActive(false);
            L4.SetActive(false);
            break;
        case "Level3":
            LH.SetActive(false);
            L0.SetActive(false);
            L1.SetActive(false);
            L2.SetActive(false);
            L3.SetActive(true);
            L4.SetActive(false);
            break;
        case "Level4":
            LH.SetActive(false);
            L0.SetActive(false);
            L1.SetActive(false);
            L2.SetActive(false);
            L3.SetActive(false);
            L4.SetActive(true);
            break;

        }
     
    }
}
