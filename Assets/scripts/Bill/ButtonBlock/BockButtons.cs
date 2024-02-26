using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BockButtons : MonoBehaviour, ObserverBlockButton
{
    public static BockButtons instance;
    public List<ObserverBlockButton> list;

    private void Awake()
    {
        instance = this;
        list = new List<ObserverBlockButton>();
    }

    public void AddButton(ObserverBlockButton btn)
    {
        list.Add(btn);
    }

    public void RemoveButton(ObserverBlockButton btn)
    {
        list.Remove(btn);
    }

    public void NotifyObserverBlockButton()
    {
        foreach(ObserverBlockButton btn in list)
        {
            btn.NotifyObserverBlockButton();
        }
    }
}
