using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public TabController tabController;
    public Image background;
    public GameObject painel;

    public void OnPointerClick(PointerEventData eventData)
    {
        tabController.OnTabSelected(this, painel);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabController.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabController.OnTabExit(this);
    }

    private void Start()
    {
        background = GetComponent<Image>();
        tabController.Subscribe(this);
    }
}
