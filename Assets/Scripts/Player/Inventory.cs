using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    GameObject backpack;
    GameObject teddybackpack;
    public List<GameObject> inventory = new List<GameObject>();

    bool readyToPickUpBackpack;
    bool GotTeddy;
    bool GotBackPack;


    // Start is called before the first frame update
    void Start()
    {
        backpack = GameObject.Find("Backpack1");
        teddybackpack = GameObject.Find("backpack2");
    }

    // Update is called once per frame
    void Update()
    {
        if(backpack == null) {
            return;
        }
        if(teddybackpack == null) {
            return;
        }

        if (getGotBackPack()) {
            backpack.SetActive(true);
            if (getGotTeddy()) {
                backpack.SetActive(false);
                teddybackpack.SetActive(true);

            }
        } else {
            backpack.SetActive(false);
            teddybackpack.SetActive(false);
        }
    }






    //Getters and Setters
    public bool getGotTeddy() {
        return GotTeddy;
    }
    public void setGotTeddy(bool value) {
        GotTeddy = value;
    }
    public bool getGotBackPack() {
        return GotBackPack;
    }
    public void setGotBackPack(bool value) {
        GotBackPack = value;
    }
    public bool getReadyToPickUpBackpack() {
        return readyToPickUpBackpack;
    }
    public void setReadyToPickUpBackpack(bool value) {
        readyToPickUpBackpack = value;
    }
}
