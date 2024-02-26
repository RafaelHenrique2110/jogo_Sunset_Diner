using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddButton : MonoBehaviour, ObserverBlockButton
{
    
    void Start()
    {
        BockButtons.instance.AddButton(this);
    }

    public void NotifyObserverBlockButton()
    {
        gameObject.GetComponent<Button>().interactable = true;
    }
}
