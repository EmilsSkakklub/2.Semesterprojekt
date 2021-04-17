using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Item : MonoBehaviour
{
    public Sprite sprite;
    public PlayerScript player;
    private Interaction interaction;
    private Inventory inventory;
    
    public string itemName;
    public string description;

    private bool isConsumable;
    private bool itemUsed;

    private float rotationSpeed = 50;
    private float amplitude = 0.2f;
    private float frequency = 1f;
    private Vector3 posOffset = new Vector3();
    private Vector3 tempPos = new Vector3();


    protected void initStart(string itemName, bool isConsumable, string description) {
        setName(itemName);
        setIsConsumable(isConsumable);
        setDescription(description);

        gameObject.tag = "Item";
        interaction = GetComponentInChildren<Interaction>();
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
        player = GameObject.Find("Player").GetComponent<PlayerScript>();

        posOffset = transform.position;
    }

    protected void initUpdate() {
        collect();
        rotateItem();
    }


    private void collect() {
        if (interaction.getStartInteraction()) {
            inventory.addItem(this);
            gameObject.SetActive(false);
        }
    }


    private void rotateItem() {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);

        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;
        transform.position = tempPos;
    }

    public abstract void useItem();


    //getters and setters
    public void setName(string itemName) {
        this.itemName = itemName;
    }
    public string getName() {
        return itemName;
    }

    public void setDescription(string description) {
        this.description = description;
    }

    public string getDescription() {
        return description;
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


}
