using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class IventarioIMGS : MonoBehaviour
{
    public ComidasOBS comida;
    [SerializeField] private Text myText;
    [SerializeField] private Image imageComponent;
    public int Quantidade = 0;
    [SerializeField] private Text myValue;
    [SerializeField] float price;
    void Start()
    {
        price = comida.valor;
        imageComponent.sprite = comida.imagem;
        AumentarQuantidade(0);
    }

    private void Update()
    {
        //Debug.Log(gameObject.name + " " + Quantidade);
    }

    public void AumentarQuantidade(int value)
    {
        Quantidade += value;
        AtualizaTexto();
    }

    public void DiminuirQuantidade(int value)
    {
        Quantidade -= value;
        AtualizaTexto();
    }

    public void AtualizaTexto()
    {
        myText.text = "x" + Quantidade;

        StringBuilder texto = new StringBuilder("");
        texto.AppendFormat("{0:C} ", price);
        if(myValue != null)
            myValue.text = texto + "/u";

    }
}
