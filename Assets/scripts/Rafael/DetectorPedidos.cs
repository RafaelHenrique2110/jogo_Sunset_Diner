using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorPedidos : MonoBehaviour
{
    public static DetectorPedidos instance;
    public int pedido, cliente, idAtendente, posiBancadaAtendente;
    public bool ocupado = false;
    public bool escolido = false;
    public bool escolidoPeloCozinhero = false;
    public bool escolidoParaAtenderAAtendente=false;
    public GameObject posiMesa;
    public GameObject pedidoPronto;
    public GameObject atendente;

    public DistribuidoraOBS distribuidoraDoAtendente;
    private void Start()
    {
        instance = this;
    }


}
