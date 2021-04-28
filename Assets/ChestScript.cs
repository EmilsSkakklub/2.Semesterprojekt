using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{

    private PlayerScript player;
    private GameObject ChestClosed;
    private GameObject ChestOpen;
    private Interaction Interaction;
    private GameObject Sword;





    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
        Sword = GameObject.Find("cSword");
        ChestClosed = GameObject.Find("Chest Closed Prefab");
        ChestOpen = GameObject.Find("Chest Open Prefab");
        Interaction = GameObject.Find("Chest_interact").GetComponent<Interaction>();
        ChestOpen.SetActive(false);

        Invoke("lol", 1);
    }


    // Update is called once per frame
    void Update()
    {
        openChest();
    }

    public void openChest() {

        if (ChestClosed.activeInHierarchy) {
            if (Interaction.getStartInteraction()) {
                ChestClosed.SetActive(false);
                ChestOpen.SetActive(true);

                Sword.SetActive(true);
            }
        }
    }

    public void lol() {
        Sword.SetActive(false);
    }

}
