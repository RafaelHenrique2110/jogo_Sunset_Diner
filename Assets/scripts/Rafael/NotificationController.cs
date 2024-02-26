using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class NotificationController : MonoBehaviour
{
    [SerializeField] float contaAgua;
    [SerializeField] float PainelConta;
    [SerializeField] float contaLuz;
    [SerializeField] float total;
    [SerializeField] float somatorioContaAgua;
    [SerializeField] float somatorioContaLuz;
    [SerializeField] Text txtluz;
    [SerializeField] Text txtagua;
    [SerializeField] Text txttotal;
    [SerializeField] string nomeContaDeAgua = "Água: ";
    [SerializeField] string nomeContaDeLuz = "Luz: ";
    [SerializeField] string nomeTotal = "Total: ";
    public int diaContas;
    public static NotificationController instance;


    void Start()
    {
        instance = this;
    }
    public void ContasParaPagar()
    {

        if (diaContas < 3)
        {
            diaContas++;
        }
        if (diaContas >= 3)
        {
            somatorioContaAgua = Random.Range(2 + EstatisticaController.Instance().dia, 4 + EstatisticaController.Instance().dia);
            somatorioContaLuz = Random.Range(2 + EstatisticaController.Instance().dia, 4 + EstatisticaController.Instance().dia);
            contaAgua += somatorioContaAgua * 100;
            contaLuz += somatorioContaLuz * 100;
            total = contaAgua + contaLuz;

            StringBuilder texto = new StringBuilder("");
            StringBuilder texto2 = new StringBuilder("");
            StringBuilder texto3 = new StringBuilder("");
            texto.AppendFormat("{0:C} ", contaAgua);
            texto2.AppendFormat("{0:C} ", contaLuz);
            texto3.AppendFormat("{0:C} ", total);

            txtagua.text = nomeContaDeAgua + texto;
            txtluz.text = nomeContaDeLuz + texto2;
            txttotal.text = nomeTotal + texto3;
        }

    }
    public void PagarConta()
    {
        SistemaFinanceiro.instance.DecreaseMoney(total);
    }
}
