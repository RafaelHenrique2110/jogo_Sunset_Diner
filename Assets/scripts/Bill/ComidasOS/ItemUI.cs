using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.DirectorModule;
using UnityEngine.Playables;
using System.Text;

public class ItemUI : MonoBehaviour
{
    public DistribuidoraOBS distribuidora;
    public Image imageComidaIcon; //Control R 2x
    public Text txtPreco, txtNome, txtbutton;
    public bool contratado;
    public float cardPrice;
    public Text cardLockPrice;
    public List<Text> infoTexts = new List<Text>();
    public List<Image> infoImages = new List<Image>();

    void Start()
    {
        BloquearItensNaTela();

        StringBuilder texto2 = new StringBuilder("");
        texto2.AppendFormat("{0:C} ", cardPrice);
        cardLockPrice.text = texto2.ToString();
        AttUI();
        //Inventario.Instance().GetDistribuidora().Add(distribuidora);
    }

    public void BloquearItensNaTela()
    {
        int quantidadeItens = distribuidora.quantidadeComidas.Count;
        //int dif = quantidadeMax - quantidadeItens;

        for(byte j = 0; j < quantidadeItens; j++)
        {
            infoImages[j].gameObject.SetActive(true);
            infoTexts[j].gameObject.SetActive(true);
            infoTexts[j].text = "x" + distribuidora.quantidadeComidas[j].ToString();
            infoImages[j].sprite = distribuidora.comidas[j].imagem;
        }
        for (byte i = (byte)(quantidadeItens); i < 5; i++)
        {
            infoImages[i].gameObject.SetActive(false);
            infoTexts[i].gameObject.SetActive(false);
        }

        //colocar panel como pai das duas listas, findobjectsbytype() image e text, for desabilita todos, for habilitando o que estiver atribuindo
    }
    
    public void AttUI()
    {
        StringBuilder texto = new StringBuilder("");

        texto.AppendFormat("{0:C} ", distribuidora.price);
        

        imageComidaIcon.sprite = distribuidora.imagem;
        txtNome.text = distribuidora.nome;
        txtPreco.text = texto.ToString();
        
    }

    public void ChangeButton()
    {
        if (!contratado)
        {
            Contratar();
            
        }
        else
        {
            Dispensar();
        }
    }
    public void Contratar()
    {   
        SistemaFinanceiro.instance.ComprarComida(distribuidora, true);
        AllFoods.Instance().distribuidoraOBS.Add(distribuidora);
        txtbutton.text = "Dispensar";
        //SistemaArmazem.instance.AtualizaUI();
        contratado = true;
        
        AlertManager.instance.ClearAlert();
        AlertManager.instance.ClearGameAlert();
        //AlertManager.instance.SetAlert("Pronto! Agora podemos iniciar um Novo Dia!");
        AlertManager.instance.SetAlert("Pronto! Agora clique no 'Inventário' e confira as quantidades e os preços da comida!");
        AlertManager.instance.setas[2].SetActive(true);
        AlertManager.instance.isContractReady = true;

        Inventario.Instance().distribuidorasContratadas.Add(distribuidora);
        Inventario.Instance().UpdateUI();
        //Material truckDistribuidora = TruckController.Instance().trucks[0].GetComponentInChildren(typeof(Plane)).GetComponent<MeshRenderer>().material;
        TruckController.Instance().AttDistruidoraTruck(distribuidora.material);
        TruckController.Instance().truck.SetActive(true);
        TruckController.Instance().timeline.GetComponent<PlayableDirector>().Play();
        
    }

    public void Dispensar()
    {
        SistemaFinanceiro.instance.ComprarComida(distribuidora, false);
        AllFoods.Instance().distribuidoraOBS.Remove(distribuidora);
        Inventario.Instance().distribuidorasContratadas.Remove(distribuidora);
        txtbutton.text = "Contratar";
        contratado = false;
        
        AlertManager.instance.ClearAlert();
        AlertManager.instance.ClearGameAlert();
        
        AlertManager.instance.SetGameAlert("É necessário contratar um Fornecedor!");
        AlertManager.instance.isContractReady = false;
    }
 
}
