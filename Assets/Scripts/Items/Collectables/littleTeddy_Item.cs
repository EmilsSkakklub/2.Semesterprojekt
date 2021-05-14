using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class littleTeddy_Item : Item
{

    public bool hasHealed;
    public float cooldownTimer;


    // Start is called before the first frame update
    void Start()
    {
        initStart("Cuddles", false, false, "Use to heal all heart (Can inly be used once).");
        cooldownTimer = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        initUpdate();
        //HealCooldown();
        
    }

    public override void useItem() {
        if (!hasHealed) {
            player.heal(8);
            hasHealed = true;
            description = "Use to heal all heart (Has been used).";
            audioManager.Play("Squeak",false, 0.2f, 1f);
        }
    }

    

    public void HealCooldown(){
        if (hasHealed) {
            if (cooldownTimer > 0) {
                cooldownTimer = 3;
                if (cooldownTimer <= 0) {
                    hasHealed = false;
                    cooldownTimer = 30f;
                }
            }
        }
    }
}
