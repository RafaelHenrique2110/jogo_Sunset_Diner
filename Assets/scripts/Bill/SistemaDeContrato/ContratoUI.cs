using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;
using System.Text;


public class ContratoUI : MonoBehaviour
{
    private static ContratoUI instance;

    public int cardId;
    public float priceLock;
    public Text priceLockText;
    public OBJS_Contrato funcionario;
    [SerializeField] private Image imageFuncionarioIcon; //Control R 2x
    [SerializeField] private Text txtSalario, txtNome, txtButton;
    [Range(0,1)] [SerializeField] private float qualidade;
    [SerializeField] private Image qualidadeImagem;
    [SerializeField] private List<Image> distribuidoraImagem;
    int idAtendenteLista = 0;
    public bool contratado = false;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        AtualizarCardCozinheiro();
    }

    public void AtualizarCardCozinheiro()
    {
        clearImages();
        StringBuilder texto = new StringBuilder("");
        StringBuilder texto2 = new StringBuilder("");
        texto.AppendFormat("{0:C} ", funcionario.salario);
        texto2.AppendFormat("{0:C} ", priceLock);

        priceLockText.text = texto2.ToString();
        imageFuncionarioIcon.sprite = funcionario.imagem;
        txtNome.text = funcionario.nome;
        //txtDescricao.text = funcionario.descricao;
        txtSalario.text = texto.ToString();

        for (int i = 0; i < funcionario.distribuidora.Count; i++)
        {
            
            distribuidoraImagem[i].gameObject.SetActive(true);
            distribuidoraImagem[i].sprite = funcionario.distribuidora[i].imagem;
        }
        
        ChangeFillAmount();
    }

    public void clearImages()
    {
        for (int i = 0; i < distribuidoraImagem.Count; i++)
        {
            distribuidoraImagem[i].gameObject.SetActive(false);
        }
    }

    public static ContratoUI Instance()
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
        if (atendente == false)
        {
            if (contratado == false)
            {
                ContratarCozinheiro();
            }
            else
            {
                DispensarCozinheiro();
            }
        }
        
    }

    public void ContratarCozinheiro()
    {
        Transform posSpawn = SistemaContrato.Instance().pos;
        
        GameObject cozinheiro = Instantiate(funcionario.model, posSpawn.transform.position, posSpawn.transform.rotation);

        Cozinhero meuCozinheiro = cozinheiro.GetComponent<Cozinhero>();
        meuCozinheiro.posiBancada = GameObject.Find("posicaoParaPrato");

        meuCozinheiro.distribuidora = funcionario.distribuidora; //corrigir
        meuCozinheiro.salario = funcionario.salario;
        meuCozinheiro.id = cardId;
        listas.instance.cozinheros[cardId] = cozinheiro;

        //SistemaContrato.Instance().IDCozinheiro();

        txtButton.text = "Demitir";

        //SistemaContrato.Instance().QuantidadeFunc(SistemaContrato.Instance().quantidadeCozinheiroMax); //deletar
        contratado = true;
        AlertManager.instance.ClearAlert();
        AlertManager.instance.ClearGameAlert();
        AlertManager.instance.SetAlert("Pronto! Agora vamos contratar um Fornecedor na aba Armazém!");
        AlertManager.instance.setas[1].SetActive(true);
        AlertManager.instance.isCookerReady = true;
        SistemaContrato.Instance().quantidadeCozAtual++;
    }

    public void DispensarCozinheiro()
    {
        //SistemaContrato.Instance().quantidadeCozAtual--;
        Destroy(listas.instance.cozinheros[cardId].gameObject); //cozinheiro sair
        listas.instance.cozinheros[cardId] = null;
        txtButton.text = "Contratar";
        contratado = false;
        
        AlertManager.instance.ClearAlert();
        AlertManager.instance.ClearGameAlert();
        
        
        AlertManager.instance.isCookerReady = false;
        SistemaContrato.Instance().quantidadeCozAtual--;

        if(SistemaContrato.Instance().quantidadeCozAtual == 0)
        {
            AlertManager.instance.SetGameAlert("É necessário contratar um Cozinheiro Antes!");
        }

    }
}
