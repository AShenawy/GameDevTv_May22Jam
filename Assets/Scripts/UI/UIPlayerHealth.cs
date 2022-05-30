using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerHealth : MonoBehaviour
{
    public Image[] playerHealthBars;

    public void SetHealthBars(int number)
    {
        if (number > playerHealthBars.Length)
        {
            number = playerHealthBars.Length;
        }

        for (int i = 0; i < playerHealthBars.Length; i++)
        {
            if (i < number)
            {
                playerHealthBars[i].gameObject.SetActive(true);
            }
            else
            {
                playerHealthBars[i].gameObject.SetActive(false);
            }
        }
    }
}
