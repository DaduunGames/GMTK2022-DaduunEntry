using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletSelection : MonoBehaviour
{
    public int DiceValue = 1;

    public Image[] Panels;

    public Color panelDeselect;
    public Color panelSelect;


    public void SelectNewBullet(int newValue)
    {
        DiceValue = newValue;
        for (int i = 0; 0 < Panels.Length; i++)
        {
            if ( i == DiceValue - 1 )
            {
                Panels[i].color = panelSelect;
            }
            else
            {
                Panels[i].color = panelDeselect;
            }
        }
    }
}
