using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alimento : MonoBehaviour
{
    public static alimento instance;
    public int cliente, id, idAtendente, atendenteEscolida;
    //public int valorComida;
    public bool recolido = false;
    public bool devorado = false;
    public bool escolido = false;
    public bool ativaEscolha = false;

    void Awake()
    {
        instance = this;
    }
    void EscolherAtendente()
    {
        if (listas.instance.atendentes.Count == 1)
        {
            atendenteEscolida = 0;
        }
        if (listas.instance.atendentes.Count > 1)
        {
            atendenteEscolida = Random.Range(0, 3); //-1
            if (listas.instance.atendentes[atendenteEscolida] != null)
            {
                idAtendente = listas.instance.atendentes[atendenteEscolida].GetComponent<Atendente>().id;
            }
            else
            {
                EscolherAtendente();
            }
        }
    }
    private void Update()
    {

        if (recolido == false && devorado == true)
        {

            for (int i = 0; i < listas.instance.atendentes.Count; i++)
            {

                if (listas.instance.atendentes[i] == null) continue;
                if (listas.instance.atendentes[i].GetComponent<Atendente>().estouOcupado == false && listas.instance.atendentes[i].GetComponent<Atendente>().current_state == Atendente.Estados.ESPERA && listas.instance.atendentes[i].GetComponent<Atendente>().id == idAtendente)
                {
                    listas.instance.atendentes[i].GetComponent<Atendente>().LimparMesa(this.gameObject);

                }




            }
        }
    }

}