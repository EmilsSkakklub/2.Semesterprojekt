using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerData
{
    public int health;
    public float stamina;


    public PlayerData(PlayerScript player) {
        //SAVE HEALTH
        health = player.HP;
        stamina = player.getStamina();
    }


}
