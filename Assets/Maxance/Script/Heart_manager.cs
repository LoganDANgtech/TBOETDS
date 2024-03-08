using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart_manager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public int health;
    public int numOfHearts;

    public Image[] hearts;
    public Sprite fullHearts;
    public Sprite emptyHeart;

    private void Update()
    {
        health = playerHealth.health;
        numOfHearts = playerHealth.maxHealth;
        if(health > numOfHearts)
        {
             health = numOfHearts;
        }


        for (int i = 0; i < hearts.Length; i++)
        {
            if(i < health)
            {
                hearts[i].sprite = fullHearts;
            }else
            {
                hearts[i].sprite = emptyHeart;
            }

            if(i < numOfHearts)
            {
                hearts[i].enabled = true;
            } else{
                hearts[i].enabled = false;
            }
        }
    }
}
