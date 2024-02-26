using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckController : MonoBehaviour
{
    private static TruckController instance;
    public GameObject truck;
    public GameObject[] distribuidoraLateral = new GameObject[2];
    public GameObject timeline;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        truck.SetActive(false);
    }
    public static TruckController Instance()
    {
        return instance;
    }

    public void AttDistruidoraTruck(Material dMaterial)
    {
        for(int i = 0; i < distribuidoraLateral.Length; i++)
        {
            distribuidoraLateral[i].GetComponent<MeshRenderer>().material = dMaterial;
        }
    }
}
