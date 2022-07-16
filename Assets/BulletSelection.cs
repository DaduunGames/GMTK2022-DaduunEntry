using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BulletSelection : MonoBehaviour
{
    public int DiceValue = 0;

    public Image[] Panels;
    public List<CanvasGroup> panelGroups;
    public CanvasGroup ReloadPanel;

    public Color panelDeselect;
    public Color panelSelect;

    public GameObject ReloadPrompt;

    private bool ShowAllPanels = false;
    public TextMeshProUGUI ExpandButtonText;

    public Animator dicePopperParent;

    private void Start()
    {
        Shotbullet();

        foreach (Image panel in Panels)
        {
            panelGroups.Add(panel.GetComponent<CanvasGroup>());
        }
    }

    private void Update()
    {
        FadePanels();
    }

    public void Shotbullet()
    {
        DiceValue = 0;
        Displaybullet(-1);
        ReloadPrompt.SetActive(true);
        dicePopperParent.SetBool("IsUp", true);
    }

    public void ReloadBullet(int newValue)
    {
        if (DiceValue == 0)
        {
            DiceValue = newValue;
            Displaybullet(newValue - 1);
            dicePopperParent.SetBool("IsUp", false);
        }
    }

    private void Displaybullet(int index)
    {
        for (int i = 0; i < Panels.Length; i++)
        {
            if (i == index)
            {
                Panels[i].color = panelSelect;
            }
            else
            {
                Panels[i].color = panelDeselect;
            }
        }
    }

    public void FadePanels()
    {
        for (int i = 0; i < panelGroups.Count; i++)
        {
            if (i != DiceValue - 1 && ShowAllPanels == false)
            {
                if (panelGroups[i].alpha > 0)
                {
                    panelGroups[i].alpha -= Time.deltaTime * 5;
                }
                else
                {
                    panelGroups[i].gameObject.SetActive(false);
                }
            }
            else
            {
                panelGroups[i].gameObject.SetActive(true);
                if (panelGroups[i].alpha < 1)
                {
                    panelGroups[i].alpha += Time.deltaTime * 5;
                }
            }
        }

        if (DiceValue == 0)
        {
            if (ReloadPanel.alpha < 1)
            {
                ReloadPanel.alpha += Time.deltaTime * 5;
            }
            ReloadPanel.gameObject.SetActive(true);
            
        }
        else
        {
            if (ReloadPanel.alpha > 0)
            {
                ReloadPanel.alpha -= Time.deltaTime * 5;
            }
            else
            {
                ReloadPanel.gameObject.SetActive(false);
            }

        }
    }

    public void ToggleShowAllPanels()
    {
        ShowAllPanels = !ShowAllPanels;

        if (ShowAllPanels)
        {
            ExpandButtonText.text = "vvv";
        }
        else
        {
            ExpandButtonText.text = "^^^";
        }
    }
}
