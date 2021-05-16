using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadEndCredits : MonoBehaviour
{
    Interaction interaction;
    // Start is called before the first frame update
    void Start()
    {
        interaction = GetComponent<Interaction>();
    }

    // Update is called once per frame
    void Update()
    {
        if (interaction.getStartInteraction()) {

        }
    }
}
