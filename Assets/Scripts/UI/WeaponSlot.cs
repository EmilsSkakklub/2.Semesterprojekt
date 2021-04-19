using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponSlot : Slot
{
    // Update is called once per frame
    void Update()
    {
        checkSlot();
    }

    public override void OnPointerDown(PointerEventData eventData) {
        base.OnPointerDown(eventData);

        if (eventData.button == PointerEventData.InputButton.Left) {
            if(isOccupied && item.getIsWeapon()) {
                player.getEquipedWeapon().dequip();
                setItem(null);
            }
        }
    }
}
