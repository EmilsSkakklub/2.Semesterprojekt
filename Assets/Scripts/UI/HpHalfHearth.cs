using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpHalfHearth : MonoBehaviour
{

    public GameObject[] FullHearts;
    public GameObject[] HalfHearts;
    public GameObject[] EmptyHearts;

    private int HP = 8;
    

    void Update()
    {

        //First [i] = 4. i.e. the array goes from 4 - 3 - 2 - 1 - 0.

        if (Input.GetKeyDown(KeyCode.R))
        {
            HP--;
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            HP++;
        }


        //First
        if (HP == 7)
        {
            FullHearts[3].gameObject.SetActive(false);
        }
        if (HP > 7)
        {
            FullHearts[3].gameObject.SetActive(true); 
        }
        if (HP > 6)
        {
            HalfHearts[3].gameObject.SetActive(true);
        }
        else if (HP == 6)
        {
            HalfHearts[3].gameObject.SetActive(false);
        }


        //Second
        else if (HP == 5)
        {
            FullHearts[2].gameObject.SetActive(false);
        }
        if (HP > 5)
        {
            FullHearts[2].gameObject.SetActive(true);  
        }
        if (HP > 4)
        {
            HalfHearts[2].gameObject.SetActive(true);
        }

        else if (HP == 4)
        {
            HalfHearts[2].gameObject.SetActive(false);
        }



        //Third
        else if (HP == 3)
        {
            FullHearts[1].gameObject.SetActive(false);
        }
        if (HP > 3)
        {
            FullHearts[1].gameObject.SetActive(true);   
        }
        if (HP > 2)
        {
            HalfHearts[1].gameObject.SetActive(true);
        }
        else if (HP == 2)
        {
            HalfHearts[1].gameObject.SetActive(false);
        }


        //fourth and Last
        else if (HP == 1)
        {
            FullHearts[0].gameObject.SetActive(false);
        }
        if (HP > 1)
        {
            FullHearts[0].gameObject.SetActive(true);       
        }
        if (HP > 0)
        {
            HalfHearts[0].gameObject.SetActive(true);
        }
        else if (HP == 0)
        {
            HalfHearts[0].gameObject.SetActive(false);
        }







    }
}
