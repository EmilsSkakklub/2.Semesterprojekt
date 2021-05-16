using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCredits : MonoBehaviour
{

    bool check = false;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Scroll", 5.5f);
        Invoke("ChangeToMainMenu", 125f);
    }

    // Update is called once per frame
    void Update()
    {
        if (check) {
            Scroll();
        }
    }
    void Scroll() {
        check = true;
        if(transform.position.y <= 76.5f) {
            transform.Translate(Vector3.up * Time.deltaTime*1.4f);
        }
    }
    void ChangeToMainMenu() {
        SceneManager.LoadScene(0);
    }
}
