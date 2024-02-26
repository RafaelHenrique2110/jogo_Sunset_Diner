using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mudacor : MonoBehaviour
{
    public Color amarelo;
    public Color vermelho;
    public bool colidi;

    Color c;
    private void Start()
    {
        listas.instance.gridOBJ.Add(this.gameObject);

    }
    void Update()
    {

        if (colidi == true)
        {
            c = vermelho;

        }

        if (colidi == false)
        {

            c = amarelo;


        }

        GetComponent<Renderer>().material.SetColor("_EmissionColor", c);

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("gridP") || other.gameObject.CompareTag("cantPlaceObj"))
        {
            colidi = true;
            GetComponentInParent<obj>().ativaBotao = false;
            //obj.instance.ativaBotao =false;
        }
    }
    void OnTriggerExit(Collider outro)
    {
        if (outro.gameObject.CompareTag("gridP") || outro.gameObject.CompareTag("cantPlaceObj"))
        {
            colidi = false;
            GetComponentInParent<obj>().ativaBotao = true;

        }

    }


}
