using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipBackpack : MonoBehaviour {
    Inventory inventory;
    Interaction interaction;
    void Start() {
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
        interaction = GetComponent<Interaction>();
    }
}