using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class ContratoUIAtendente : MonoBehaviour
{
    private static ContratoUIAtendente instance;

    public int cardId;
    public float priceLock;
    public Text priceLockText;
    public OBJS_Contrato funcionario;
    [SerializeField] private Image imageFuncionarioIcon; //Control R 2x
    [SerializeField] private Text txtSalario, txtNome, txtDescricao, txtButton;
    [Range(0, 1)][SerializeField] private float qualidade;
    [SerializeField] private Image qualidadeImagem;
    int idAtendenteLista = 0;
    public bool contratado = false;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        StringBuilder texto = new StringBuilder("");
        texto.AppendFormat("{0:C} ", priceLock);
        priceLockText.text = texto.ToString();

        AtualizarCardAtendente();
    }

    public void AtualizarCardAtendente()
    {
        StringBuilder texto = new StringBuilder("");
        texto.AppendFormat("{0:C} ", funcionario.salario);

        imageFuncionarioIcon.sprite = funcionario.imagem;
        txtNome.text = funcionario.nome;
        //txtDescricao.text = funcionario.descricao;
        txtSalario.text =  texto.ToString();
        ChangeFillAmount();
    }

    public static ContratoUIAtendente Instance()
    {
        return instance;
    }

    public void ChangeFillAmount()
    {
        qualidade = funcionario.qualidade;
        qualidadeImagem.fillAmount = qualidade / 5;
    }

    public void ChangeButton(bool atendente)
    {
        if (atendente)
        {
            if (contratado == false)
            {
                ContratarAtendente();
            }
            else
            {
                DispensarAtendente();
            }
        }

    }


    public void ContratarAtendente()
    {

        Transform posSpawn = SistemaContrato.Instance().pos;
       

        GameObject funcionarioInstaciado = Instantiate(funcionario.model, posSpawn.transform.position, posSpawn.transform.rotation);
        Atendente meuFuncionario = funcionarioInstaciado.GetComponent<Atendente>();
        meuFuncionario.id = cardId;
        meuFuncionario.salario = funcionario.salario;
        listas.instance.atendentes[cardId] = funcionarioInstaciado;
            
        //SistemaContrato.Instance().IDAtendente();

        txtButton.text = "Demitir";

        //SistemaContrato.Instance().QuantidadeFunc(SistemaContrato.Instance().quantidadeAtendenteMax); //deletar
        Debug.Log(cardId);

        contratado = true;
        AlertManager.instance.ClearAlert();
        AlertManager.instance.ClearGameAlert();
        AlertManager.instance.SetAlert("Pronto! Agora vamos contratar um Cozinheiro");
        AlertManager.instance.isWaitressReady = true;

        
    }

    public void DispensarAtendente()
    {
        Espera.instance.esperaposi[listas.instance.atendentes[cardId].GetComponent<Atendente>().espera].GetComponent<posiEspera>().ocupado = false;
        SistemaContrato.Instance().quantidadeFuncAtual--;
        Destroy(listas.instance.atendentes[cardId].gameObject); //atendente sair
        listas.instance.atendentes[cardId] = null;
        txtButton.text = "Contratar";
        contratado = false;

        AlertManager.instance.ClearAlert();
        AlertManager.instance.ClearGameAlert();

        AlertManager.instance.SetGameAlert("É necessário contratar uma Atendente!");
        AlertManager.instance.isWaitressReady = false;

    }
}
