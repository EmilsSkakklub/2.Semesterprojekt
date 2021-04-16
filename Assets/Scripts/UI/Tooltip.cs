using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    public Text text1;
    public Text text2;

    // Start is called before the first frame update
    void Start()
    {
        text1 = GameObject.Find("TextField1").GetComponent<Text>();
        text2 = GameObject.Find("TextField2").GetComponent<Text>();
    }


    public void setText1(Item item) {
        if(item == null) {
            text1.text = " ";
        }
        else {
            text1.text = item.getName();
        }
    }

    public void setText2(Item item) {
        if (item == null) {
            text2.text = " ";
        }
        else {
            text2.text = item.getDescription();
        }
    }

}
