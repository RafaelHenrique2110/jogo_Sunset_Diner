using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject sideMenu;
    private obj[] objsCenario;
    List<Canvas> objsCanvas = new List<Canvas>();
    public Animator anim;
    public Animator cd;
    private void Awake()
    {
        instance = this;
    }

    public void animCD(bool ON)
    {
        if(ON)
        {
            cd.Play("CDout");
        }
        else
        {
            cd.Play("CDin");
        }
    }

    public void DesativaCanvas()
    {
        objsCenario = FindObjectsOfType(typeof(obj)) as obj[];
        Debug.Log(objsCenario.Length);
        objsCanvas = new List<Canvas>();

        for (int i = 0; i < objsCenario.Length; i++)
        {
            Canvas canvasObj = objsCenario[i].GetComponentInChildren<Canvas>();
            canvasObj.gameObject.SetActive(false);
            objsCanvas.Add(canvasObj);
        }
    }



    public void AtivaCanvas()
    {
        if (objsCanvas.Count > 0)
        {
            for (int i = 0; i < objsCanvas.Count; i++)
            {
                objsCanvas[i].gameObject.SetActive(true);
            }
        }
    }
    public void ActivePainel(AtivaPainel paineis)
    {
        paineis.paineilAtivar.SetActive(true);
        for (int i = 0; i < paineis.paineilDesativar.Length; i++)
        {
            paineis.paineilDesativar[i].gameObject.SetActive(false);
        }
    }



}
