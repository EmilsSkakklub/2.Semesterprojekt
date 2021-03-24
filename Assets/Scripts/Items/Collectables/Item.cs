using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Item : MonoBehaviour
{
    private Interaction interaction;
    private Inventory inventory;

    protected string itemName;
    public Sprite sprite;

    protected void initStart() {
        gameObject.tag = "Item";

        interaction = GetComponent<Interaction>();
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
    }

    protected void initUpdate(string itemName) {
        setName(itemName);
        collect();
    }

    private void collect() {
        if (interaction.getStartInteraction()) {
            inventory.addItem(this);
            gameObject.SetActive(false);
        }
    }




    //getters and setters
    public void setName(string itemName) {
        this.itemName = itemName;
    }
    public string getName() {
        return itemName;
    }
    
    public Sprite getSprite() {
        return sprite;
    }

}
