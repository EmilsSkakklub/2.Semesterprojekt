using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    private bool StartInteraction;

    private void Start() {
        gameObject.tag = "Interactable";
    }


    public bool getStartInteraction() {
        return StartInteraction;
    }
    public void setStartInteraction(bool StartInteraction) {
        this.StartInteraction = StartInteraction;
    }

}
