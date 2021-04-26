using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{

    private GameObject ChestClosed;
    private GameObject ChestOpen;
    private Interaction Interaction;
    private GameObject Sword;
    private GameObject WaterBottle;
    private GameObject Apple;

    private List<GameObject> itempool = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

        Sword = GameObject.Find("cSword");
        
        ChestClosed = GameObject.Find("Chest Closed Prefab");
        ChestOpen = GameObject.Find("Chest Open Prefab");

        Interaction = GameObject.Find("Chest_interact").GetComponent<Interaction>();

        itempool.Add(Sword);

        Sword.SetActive(false);
        ChestOpen.SetActive(false);
        

    }


    // Update is called once per frame
    void Update()
    {
        if(Interaction.getStartInteraction())
        {
            ChestClosed.SetActive(false);
            ChestOpen.SetActive(true);

            itempool[0].SetActive(true);

        }
    }
}
