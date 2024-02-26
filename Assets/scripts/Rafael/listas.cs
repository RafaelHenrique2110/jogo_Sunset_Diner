using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class listas : MonoBehaviour
{
    public static listas instance;
   
     public List<GameObject> cliente;
    public List<GameObject> clienteEsperando;
    public List<GameObject> CadeirasOcupadas;
    public List<GameObject> bancada;
    public List<GameObject> armario;
    public List<GameObject> geladeira;
    public List<GameObject> forno;
    public List<GameObject> mesaSuja;
    public List<GameObject> atendentes;
    public List<GameObject> cadeiras;
    public List<GameObject> mesa;
    public List<GameObject> cozinheros;
    public List<GameObject> canvas;
    public List<GameObject> gridOBJ;

    void Start()
    {
        instance = this;
       
    }

    public void RemoveDaLista(GameObject obj, List<GameObject> list)
    {
        list.Remove(obj);
    }

    public void ResetClient()
    {

        for (int i = 0; i < cliente.Count; i++)
        {

            Destroy(cliente[i]);

        }

        for (int j = 0; j < atendentes.Count; j++)
        {

            if (atendentes[j] != null)
            {
                atendentes[j].GetComponent<Atendente>().novoEstado = Atendente.Estados.START;

            }
            if (cozinheros[j] != null)
            {
                cozinheros[j].GetComponent<Cozinhero>().novoEstado = Cozinhero.Estados.IRTRABALHAR;
            }

        }

        for(int k = 0; k < CadeirasOcupadas.Count; k++)
        {
            cadeiras.Add(CadeirasOcupadas[k]);
        }


    }
}
