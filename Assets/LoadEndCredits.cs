using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadEndCredits : MonoBehaviour
{
    Interaction interaction;
    Animator transition;
    // Start is called before the first frame update
    void Start()
    {
        transition = GameObject.Find("Crossfade").GetComponent<Animator>();
        interaction = GetComponent<Interaction>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(EndCreditLoad());
    }
    IEnumerator EndCreditLoad() {
        if (interaction.getStartInteraction()) {
            transition.SetBool("Start", true);
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(5);
        }
    }
}
