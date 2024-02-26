using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class UIDinheiro : MonoBehaviour
{
    public static UIDinheiro instance;
    public Color originalColor;
    public List<Text> txtDinheiro;
    public StringBuilder texto;
    public StringBuilder texto2;
    private void Awake()
    {
        instance = this;
        //originalColor = GetComponent<Text>().color;
    }
    
    public void UpdateUI(float valor)
    {
        for(int i = 0; i < txtDinheiro.Count; i++)
        {
            if (valor <= 0)
            {
                //txtDinheiro[i].color = Color.red;
            }
            else
            {
                //txtDinheiro[i].color = originalColor;
            }  
        }

        texto = new StringBuilder("Saldo: ");
        texto2 = new StringBuilder("");
        texto.AppendFormat("{0:C} ", valor);
        texto2.AppendFormat("{0:C} ", valor);
        txtDinheiro[0].text = texto2.ToString();
        txtDinheiro[1].text = texto.ToString(); //$"Saldo: {(float)valor}";

        /* txtDinheiro[0].text = $"{valor}";
        txtDinheiro[1].text = string.Format("{ 0:0.00}", valor); //$"Saldo: {(float)valor}";*/

        if (valor >= SistemaFinanceiro.instance.LIMITE)
            BockButtons.instance.NotifyObserverBlockButton();
    }
}
