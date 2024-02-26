using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComidasController : MonoBehaviour
{
    public List<DistribuidoraOBS> objsLista = new List<DistribuidoraOBS>();

    private void Awake()
    {
        RemoveQuatidade();
    }
    public void RemoveQuatidade()
    {
        foreach (DistribuidoraOBS obj in objsLista)
        {
            //obj.quantidade = 0;
        }
    }
}
