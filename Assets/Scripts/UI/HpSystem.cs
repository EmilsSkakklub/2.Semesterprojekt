using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpSystem : MonoBehaviour
{
    public int health;
    public int NumOfHearts;

    public Image[] hearts;
    public Sprite FullHeart;
    public Sprite HalfHeart;
    public Sprite EmptyHeart;


    private void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {

            if(i < health)
            {
                hearts[i].sprite = FullHeart;
            }
            else
            {
                hearts[i].sprite = EmptyHeart;
            }




            //management of maximum amount of hearts.
            if(i < NumOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }


            //Cant have more health than amount of hearts.
            if(health > NumOfHearts)
            {
                health = NumOfHearts;
            }



        }
    }

}
