using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EstatisticaController : MonoBehaviour
{
    private static EstatisticaController instance;
    [SerializeField] private int clientesA;
    [SerializeField] private int clientesNA;
    [SerializeField] private float despesas;
    public int dia = 1;
    [SerializeField] private List<Text> textos;

    private void Awake()
    {
        instance = this;
        Time.timeScale = 0;
    }

    private void Start()
    {
        DiaContador();
    }

    public static EstatisticaController Instance()
    {
        return instance;
    }  
    public void ClientesA()
    {
        clientesA++;
        textos[0].text = "Clientes Atendido: " + clientesA.ToString();
    }

    public void ClientesNA()
    {
        clientesNA++;
        textos[1].text = "Clientes Não Atendidos: " + clientesNA.ToString();
    }

    public void AddDespesas(float valor)
    {
        despesas += valor;
        textos[2].text = "Despesas: " + despesas.ToString();
    }

    public void DecreaseDespesas(float valor)
    {
        despesas -= valor;
        textos[2].text = "Despesas: " + despesas.ToString();
    }

    public void ResetDespesas()
    {
        despesas = 0;
        textos[2].text = "Despesas: " + despesas.ToString();
    }

    public void DiaContador()
    {
        textos[3].text = "Dia: " + dia.ToString();
    }
}
