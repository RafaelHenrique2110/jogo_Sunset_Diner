using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class Refresh : MonoBehaviour
{
    [SerializeField] private GameObject telaCozinheiro;
    [SerializeField] private GameObject telaAtendente;
    [SerializeField] private GameObject telaDistribuidora;
    [SerializeField] private GameObject[] cardDistribuidoras;
    [SerializeField] private Text txtMoney;
    [SerializeField] int nClicks;
    int n;
    [SerializeField] float r = 100.00f;

    private void Start()
    {
        StringBuilder texto = new StringBuilder("");
        texto.AppendFormat("{0:C} ", r);

        txtMoney.text = texto.ToString(); 
    }
    public void RefreshBTN()
    {
        if(SistemaFinanceiro.instance.GetMoney() >= r)
        {
            
            IncreaseMoney();
            
            
            if (telaCozinheiro.activeSelf)
            {
                for (int i = 0; i < FuncLists.Instance().cozinheirosCards.Count; i++)
                {
                    n = Random.Range(0, FuncLists.Instance().cozinheirosOBJ.Count);

                    if(FuncLists.Instance().cozinheirosCards[i].contratado == false)
                    {
                        FuncLists.Instance().cozinheirosCards[i].funcionario = FuncLists.Instance().cozinheirosOBJ[n];
                        FuncLists.Instance().cozinheirosCards[i].AtualizarCardCozinheiro();
                    }
                }
            }
            if (telaAtendente.activeSelf)
            {
                for (int i = 0; i < FuncLists.Instance().atendentesCards.Count; i++)
                {
                    n = Random.Range(0, FuncLists.Instance().atendentesOBJ.Count);

                    if(FuncLists.Instance().atendentesCards[i].contratado == false)
                    {
                        FuncLists.Instance().atendentesCards[i].funcionario = FuncLists.Instance().atendentesOBJ[n];
                        FuncLists.Instance().atendentesCards[i].AtualizarCardAtendente();
                    }
                }

                telaAtendente.GetComponentInChildren<ContratoUIAtendente>().funcionario = FuncLists.Instance().atendentesOBJ[n];
            }
            if (telaDistribuidora.activeSelf) //tratar concição distribuidoras iguais
            {
                for(int i = 0; i < cardDistribuidoras.Length; i++)
                {
                    n = Random.Range(0, AllFoods.Instance().distribuidoraOBS.Count);

                    if(cardDistribuidoras[i].GetComponent<ItemUI>().contratado == false)
                    {
                        cardDistribuidoras[i].GetComponent<ItemUI>().distribuidora = AllFoods.Instance().distribuidoraOBS[n];
                        cardDistribuidoras[i].GetComponent<ItemUI>().AttUI();
                        cardDistribuidoras[i].GetComponent<ItemUI>().BloquearItensNaTela();
                    }
                }
                    
            }
        }
    }

    public void IncreaseMoney()
    {
        

        nClicks++;
       

        SistemaFinanceiro.instance.DecreaseMoney(r);
        
        r = 100 * nClicks;

        StringBuilder texto = new StringBuilder("");
        texto.AppendFormat("{0:C} ", r);
        txtMoney.text = texto.ToString();


    }
}
/**/