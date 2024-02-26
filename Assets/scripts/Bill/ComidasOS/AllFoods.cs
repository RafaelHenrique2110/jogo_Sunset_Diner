using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllFoods : MonoBehaviour
{
    private static AllFoods instance;
    //public List<ComidasOBS> comidasOBs;
    public List<DistribuidoraOBS> distribuidoraOBS;
    public static AllFoods Instance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }
}
