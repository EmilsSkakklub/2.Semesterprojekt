using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Slot : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler {
    
    public int index;
    public bool isOccupied;
    public Item item;
    public Image spriteRender;
    public Tooltip tooltip;
    public PlayerScript player;
    public AudioManager audioManager;

    void Awake() {
        spriteRender = gameObject.transform.Find("SpriteRender").GetComponent<Image>();
        tooltip = GameObject.Find("Tooltip").GetComponent<Tooltip>();
        audioManager = GameObject.Find("GameManager").GetComponent<AudioManager>();
    }

    


    void Update() {
        checkSlot();
    }


    public virtual void OnPointerDown(PointerEventData eventData) {
        //left click in the inventory
        if(eventData.button == PointerEventData.InputButton.Left) {
            if (isOccupied) {
                item.useItem();
                item.setItemUsed(true);
            }
            else if (!isOccupied) {
                audioManager.Play("InteractSound", false, 0.1f, 1f);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (!isOccupied) {
            tooltip.setText1(null);
            tooltip.setText2(null);
            
        }
        if (isOccupied) {
            tooltip.setText1(item);
            tooltip.setText2(item);
        }
    }


    protected void checkSlot() {
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
