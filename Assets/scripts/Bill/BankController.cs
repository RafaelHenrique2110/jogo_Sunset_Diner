using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
public class BankController : MonoBehaviour
{
    private static BankController instance;
    [SerializeField] float loan1;
    [SerializeField] float loan2;
    [SerializeField] float loan3;

    [SerializeField] Text loan1txt;
    [SerializeField] Text loan2txt;
    [SerializeField] Text loan3txt;

    [SerializeField] Button loan1btn;
    [SerializeField] Button loan2btn;
    [SerializeField] Button loan3btn;

    [SerializeField] float loantotal;
    [SerializeField] Text loantotaltxt;
    private void Awake()
    {
        instance = this;
    }

    public static BankController Instance()
    {
        return instance;
    }

    private void Start()
    {
        AttLoan(loan1txt, loan1);
        AttLoan(loan2txt, loan2);
        AttLoan(loan3txt, loan3);
        AttTotal();
    }

    public void AttLoan(Text txt, float valor)
    {
        StringBuilder texto = new StringBuilder("");
        texto.AppendFormat("{0:C} ", valor);
        txt.text = texto.ToString(); // "$" + valor;
    }
    public void AttTotal()
    {
        StringBuilder texto = new StringBuilder("");
        texto.AppendFormat("{0:C} ", loantotal);
        loantotaltxt.text = texto.ToString();
    }
    public void TakeLoan(Button btn)
    {
        if(btn == loan1btn)
        {
            SistemaFinanceiro.instance.AddMoney(loan1);
            AttLoan(loan1txt, loan1);
            SomarLoan(loan1);
            
        }
            
        if (btn == loan2btn)
        {
            SistemaFinanceiro.instance.AddMoney(loan2);
            AttLoan(loan2txt, loan2);
            SomarLoan(loan2);
        }
            
        if (btn == loan3btn)
        {
            SistemaFinanceiro.instance.AddMoney(loan3);
            AttLoan(loan3txt, loan3);
            SomarLoan(loan3);
        } 
    }

    public void PayLoan()
    {
        SistemaFinanceiro.instance.DecreaseMoney(loantotal);
        loan1btn.interactable = true;
        loan2btn.interactable = true;
        loan3btn.interactable = true;
        loantotal = 0;
        AttTotal();
    }

    private void SomarLoan( float valor)
    {
        loantotal += valor;
        AttTotal();
    }

    public void MultiplicarLoan()
    {
        int dia = EstatisticaController.Instance().dia;
        loantotal += loantotal * (dia / 10);
        AttTotal();
    }
    
}
