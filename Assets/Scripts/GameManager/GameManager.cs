using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private PlayerScript playerScript;
    private Inventory inventory;




    private static GameManager instance = null;

    //singelton pattern
    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else if (instance != null) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        InitStart();
    }

    private void InitStart() {
        playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
    }


}
