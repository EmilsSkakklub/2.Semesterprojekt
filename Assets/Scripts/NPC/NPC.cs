using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPC : MonoBehaviour
{
    public bool isInteractable;


    protected void InitStart(bool isInteractable) {
        setIsInteractable(isInteractable);
    }

    protected void InitUpdate() {

    }


    //getters and setters
    public bool getIsInteractable() {
        return isInteractable;
    }
    public void setIsInteractable(bool isInteractable) {
        this.isInteractable = isInteractable;
    }

}
