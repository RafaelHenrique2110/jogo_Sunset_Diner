using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurnoController : MonoBehaviour
{
    private static TurnoController instance;

    [SerializeField] private List<GameObject> inicioHUD; //mudar para ObjectScene
    [SerializeField] private List<GameObject> finalHUD;

    private void Awake()
    {
        instance = this;
    }
    
    public static TurnoController Instance()
    {
        return instance;
    }
    public void ShowHudInicial(bool v)
    {
        for(int i = 0; i < inicioHUD.Count; i++)
        {
            //inicioHUD[i].SetActive(v);
            
        }
        
    }
    public void ShowHudFinal(bool v)
    {
        for (int i = 0; i < inicioHUD.Count; i++)
        {
            finalHUD[i].GetComponent<Animator>().Play("CanvasAparecer");
            TimeController.instance.CurrentTime = DateTime.Now.Date  + TimeSpan.FromHours(7); ;
        }
    }
}
