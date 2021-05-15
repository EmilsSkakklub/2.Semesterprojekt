using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public bool StartInteraction;

    private void Awake() {
        gameObject.tag = "Interactable";
    }


    public bool getStartInteraction() {
        return StartInteraction;
    }

    public void setStartInteraction(bool StartInteraction) {
        this.StartInteraction = StartInteraction;
    }

}
