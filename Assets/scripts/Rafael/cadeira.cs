using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;



public  class cadeira : MonoBehaviour
{

    public bool ocupado = false;
    public bool naMesa = false;
    public bool vendida = false;
    public LookAtConstraint lookCamera;
    public GameObject camera;
    public GameObject canvasAlerta;



    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.Find("Main Camera");
        listas.instance.cadeiras.Add(this.gameObject);
       
    
    }

    // Update is called once per frame
    void Update()
    {
      
       
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("cliente"))
        {
            ocupado = true;
            listas.instance.CadeirasOcupadas.Add(this.gameObject);
        }
        /*if (other.CompareTag("mesa") && naMesa ==false)
        {
            listas.instance.cadeiras.Add(this.gameObject);
            naMesa = true;
            canvasAlerta.SetActive(false);

        }*/

    }
    private void OnTriggerExit(Collider other)
    {
        listas.instance.CadeirasOcupadas.Remove(this.gameObject);
        ocupado = false;
        if (other.CompareTag("cliente"))
        {
            listas.instance.CadeirasOcupadas.Remove(this.gameObject);
            ocupado = false;
            
        }
        /*if (other.CompareTag("mesa"))
        {
            canvasAlerta.SetActive(true);
            listas.instance.cadeiras.Remove(this.gameObject);
            naMesa = false;

        }*/

    }



}
