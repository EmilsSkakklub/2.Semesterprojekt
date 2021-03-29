using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Slot : MonoBehaviour, IPointerDownHandler {
    
    public int index;
    public bool isOccupied;
    public Item item;
    public Image spriteRender;

    void Awake() {
        spriteRender = gameObject.transform.Find("SpriteRender").GetComponent<Image>();
    }

    


    private void Update() {
        checkSlot();
    }


    public void OnPointerDown(PointerEventData eventData) {
        //left click in the inventory
        if(eventData.button == PointerEventData.InputButton.Left) {
            if (isOccupied) {
                item.useItem();
                item.setItemUsed(true);
            }
            else if (!isOccupied) {
                print(index + " is empty");
            }
        }
    }


    private void checkSlot() {
        if(item == null) {
            setIsOccupied(false);
            spriteRender.enabled = false;
        }
        else if(item != null) {
            setIsOccupied(true);
            spriteRender.enabled = true;
            spriteRender.sprite = item.getSprite();
        }
    }




    //getters and setters
    public int getIndex() {
        return index;
    }
    public void setIndex(int index) {
        this.index = index;
    }

    public bool getIsOccupied() {
        return isOccupied;
    }
    public void setIsOccupied(bool isEmpty) {
        this.isOccupied = isEmpty;
    }

    public Item getItem() {
        return item;
    }
    public void setItem(Item item) {
        this.item = item;
    }

    public Sprite getSpriterender() {
        return spriteRender.sprite;
    }
    public void setSpriteRender(Sprite sprite) {
        spriteRender.sprite = sprite;
    }

}
