using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faxineira : MonoBehaviour
{
    
    Vector3 dir;
    public float speed;
    int posiLista;
    public bool levarVazilhaSuja = false;
    public bool limparMesa = false;
    public GameObject mao;
     
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (listas.instance.mesaSuja.Count > 0 && limparMesa == true )
        {
            
            LimparMesa();
        }
        if (levarVazilhaSuja == true)
        {
            LevarVazilhaSuja();
        }

    }
    public void LimparMesa()
    {
       
        for ( int i = 0; i < listas.instance.mesaSuja.Count; i++)
        {
            posiLista = i;
            if(listas.instance.mesaSuja[i].GetComponent<alimento>().recolido == false && listas.instance.mesaSuja[i].GetComponent<alimento>().devorado == true)
            {
               
               
                
                    dir = listas.instance.mesaSuja[i].transform.position - transform.position;
               

                if (dir.magnitude > 1)
                {

                    transform.position += dir.normalized * speed * Time.deltaTime;
                    
                }

                if (dir.magnitude <= 1)
                {

                   
                    levarVazilhaSuja = true;



                }
                Debug.Log("pegar louça suja");

            }


        }

    }
    void LevarVazilhaSuja()
    {
        limparMesa = false;
        

            listas.instance.mesaSuja[posiLista].transform.position = mao.transform.transform.position;
            listas.instance.mesaSuja[posiLista].transform.parent = mao.transform;
            listas.instance.mesaSuja[posiLista].GetComponent<alimento>().recolido = true;
           
            dir = listas.instance.bancada[0].transform.position - transform.position;
            if (dir.magnitude > 2)
            {
                
                
                    transform.position += dir.normalized * speed * Time.deltaTime;
                
                
            }
            if (dir.magnitude <= 2)
            {

                listas.instance.mesaSuja[posiLista].transform.position = listas.instance.bancada[0].GetComponent<Bancada>().posiBancada.transform.position;
                listas.instance.mesaSuja[posiLista].transform.parent = null;
                listas.instance.mesaSuja.RemoveAt(posiLista);
                
                limparMesa = true;
                levarVazilhaSuja = false;

            }
        
       
        
    }
}
