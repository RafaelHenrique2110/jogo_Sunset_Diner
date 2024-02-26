using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedidos : MonoBehaviour
{
    public static Pedidos instance;
    public int numeroPedido, cliente;
    public bool foiPreparado = false;
    void Start()
    {
       instance = this;
    }

}
