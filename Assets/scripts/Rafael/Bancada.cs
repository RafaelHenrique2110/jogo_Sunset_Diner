using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bancada : MonoBehaviour
{
    public static Bancada instance;
    public GameObject posiBancada;
    public List<GameObject> posiEsperaCozinhero;
    public List<GameObject> posicaoPegar;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
