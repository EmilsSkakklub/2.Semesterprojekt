using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Item : MonoBehaviour
{
    private Interaction interaction;
    private Inventory inventory;

    public Sprite sprite;

    public string itemName;
    
    public int amount;

    public bool isConsumable;
    public bool isStackable;
    public bool itemUsed;
    

    protected void initStart(string itemName, bool isConsumable, bool isStackable) {
        setName(itemName);
        setIsConsumable(isConsumable);
        setIsStackable(isStackable);

        gameObject.tag = "Item";

        interaction = GetComponent<Interaction>();
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
    }

    protected void initUpdate() {
        collect();
    }

    private void collect() {
        if (interaction.getStartInteraction()) {
            inventory.addItem(this);
            gameObject.SetActive(false);
        }
    }


    public abstract void useItem();




    //getters and setters
    public void setName(string itemName) {
        this.itemName = itemName;
    }
    public string getName() {
        return itemName;
    }

    public void setIsConsumable(bool isConsumable) {
        this.isConsumable = isConsumable;
    }
    public bool getIsConsumable() {
        return isConsumable;
    }

    public void setItemUsed(bool itemUsed) {
        this.itemUsed = itemUsed;
    }
    public bool getItemUsed() {
        return itemUsed;
    }

    public Inventory getInventory() {
        return inventory;
    }


    public Sprite getSprite() {
        return sprite;
    }

    public void setSprite(Sprite sprite) {
        this.sprite = sprite;
    }

    public bool getIsStackable() {
        return isStackable;
    }
    public void setIsStackable(bool isStackable) {
        this.isStackable = isStackable;
    }

}
