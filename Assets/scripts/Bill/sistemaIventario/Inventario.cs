using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventario : MonoBehaviour
{
    private static Inventario instance;
    public List<DistribuidoraOBS> distribuidorasContratadas= new List<DistribuidoraOBS>();
    public IventarioIMGS[] slotInventario;
    
    private void Awake()
    {
        instance = this;
    }

    public static Inventario Instance()
    {
        return instance;
    }

    public List<DistribuidoraOBS> GetDistribuidora()
    {
        return distribuidorasContratadas;
    }

    public void UpdateUI()
    {
        
        foreach (DistribuidoraOBS distribuidora in distribuidorasContratadas)
        {
            for(int i = 0; i < distribuidora.comidas.Count; i++)
            {
                
                ComidasOBS comidasDistribuidora = distribuidora.comidas[i];
                foreach(IventarioIMGS slotComida in slotInventario)
                {
                    
                    if (slotComida.comida == comidasDistribuidora)
                    {
                        slotComida.AumentarQuantidade(distribuidora.quantidadeComidas[i]);
                        
                    }
                }
            }
        }
    }

    public void UpdateUIConsumir(ComidasOBS comidaCliente)
    {
        foreach (IventarioIMGS slotComida in slotInventario)
        {
            //Debug.Log("atualizei");
            if (slotComida.comida == comidaCliente)
            {
                slotComida.DiminuirQuantidade(1);
            }
        }
    }
    public IventarioIMGS VerificarComida(ComidasOBS comida)
    {
        for(int i=0; i < slotInventario.Length; i++)
        {
            if(comida.nome == slotInventario[i].comida.nome)
            {
                Debug.Log(slotInventario[i].comida.nome);
                return slotInventario[i];
            }
        }
        Debug.Log("nome nao encontrado");
        return null;
    }


}
