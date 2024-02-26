using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Espera : MonoBehaviour
{
    public static Espera instance;

    public List<GameObject> esperaposi;
    


    void Start()
    {
        instance = this;

    }
}
