using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class botaoSpawnerObj : MonoBehaviour
{
    public static botaoSpawnerObj instance;
    public GameObject mesa;
    public GameObject cadeira;
    GameObject tipocadeira;
   

    void Start()
    {
        instance = this;
    }

        // Update is called once per frame
    public void SpawnerMesa(){

        Instantiate (mesa,transform.position,transform.rotation);
    
    }
    public void SpawnerCadeira()
    {
        
        tipocadeira = Instantiate(cadeira, transform.position, transform.rotation);
        
        for(int i = 0; i< listas.instance.cliente.Count; i++)
        {
            listas.instance.cadeiras.Add(this.gameObject);
        }
            
        


    }

}
