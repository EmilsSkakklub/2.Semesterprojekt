using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipTeddy : MonoBehaviour {
    Inventory inventory;
    Interaction interaction;
    void Start() {
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
        interaction = GetComponent<Interaction>();
    }

    void Update() {

    }
}

