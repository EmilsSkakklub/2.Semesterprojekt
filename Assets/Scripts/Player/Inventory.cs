using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    private PlayerScript player;
    private Image inventoryUI;

    public List<Slot> inventory = new List<Slot>();


    GameObject backpack;
    GameObject teddybackpack;
    bool readyToPickUpBackpack;
    bool GotTeddy;
    bool GotBackPack;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
        inventoryUI = GameObject.Find("InventoryUI").GetComponent<Image>();

        inventoryUI.gameObject.SetActive(false);

        backpack = GameObject.Find("Backpack1");
        teddybackpack = GameObject.Find("backpack2");


        initializeInventorySlots();
    }

    // Update is called once per frame
    void Update()
    {
        backpackTeddy();
        toggleInventory();
    }

    
    private void initializeInventorySlots() {
        for(int i = 0; i < inventoryUI.transform.childCount; i++) {
            inventory.Add(inventoryUI.transform.GetChild(i).GetComponent<Slot>());
            inventory[i].setIndex(i + 1);
        }
    }


    //inventory
    private void toggleInventory() {
        //if inventory is open, pause game and show mouse 
        if (player.getOpenInventory()) {
            Cursor.lockState = CursorLockMode.Confined;
            inventoryUI.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else if (!player.getOpenInventory()) {
            inventoryUI.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
        }
    }







    private void backpackTeddy() {
        if (backpack == null) {
            return;
        }
        if (teddybackpack == null) {
            return;
        }

        if (getGotBackPack()) {
            backpack.SetActive(true);
            if (getGotTeddy()) {
                backpack.SetActive(false);
                teddybackpack.SetActive(true);

            }
        }
        else {
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
