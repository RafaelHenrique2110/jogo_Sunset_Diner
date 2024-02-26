using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{
    public List<TabButton> tabButtons;
    public Sprite tabIdle;
    public Sprite tabHover;
    public Sprite tabActive;
    public TabButton tabSelected;
    //public List<GameObject> objectToSwap;
    public GameObject actualPainel;

    private void Start()
    {
        //Clear();
        //tabSelected = tabButtons[0];
        //tabSelected.background.sprite = tabActive;
    }
    public void Subscribe(TabButton button)
    {
        if(tabButtons == null)
        {
            tabButtons = new List<TabButton>();
        }

        tabButtons.Add(button);
    }

    public void OnTabEnter(TabButton button)
    {
        if(!tabSelected)
            button.background.sprite = tabHover;
    }

    public void OnTabExit(TabButton button)
    {
        if(!tabSelected)
            button.background.sprite = tabIdle; 
    }

    public void OnTabSelected(TabButton button, GameObject painel)
    {
        /*if(button != null)
        {
            tabSelected.background.sprite = tabIdle;
            tabSelected = button;
            tabSelected.background.sprite = tabActive;
        }*/
        Clear();
        button.background.sprite = tabActive;
        if (painel != null)
        {
            actualPainel.SetActive(false);
            actualPainel = painel;
            actualPainel.SetActive(true);
        }
    }

    void Clear()
    {
        foreach(TabButton b in tabButtons)
        {
            b.background.sprite = tabIdle;
        }
    }
}
