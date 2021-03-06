using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class house_interact : MonoBehaviour
{

    Interaction interaction;
    LB_dialog LB_dialog;

    // Start is called before the first frame update
    void Start()
    {
        interaction = GameObject.Find("House_Interact").GetComponent<Interaction>();
        LB_dialog = GameObject.Find("LB_Interact").GetComponent<LB_dialog>();
    }

    // Update is called once per frame
    void Update()
    {
        if (interaction.getStartInteraction() && LB_dialog.readyForSequence2) {
            LB_dialog.dialogSequence++;
            LB_dialog.readyForSequence2 = false; 
        }
    }
}
