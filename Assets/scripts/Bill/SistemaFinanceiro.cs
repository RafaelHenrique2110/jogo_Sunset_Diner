using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class SistemaFinanceiro : MonoBehaviour
{
    public static SistemaFinanceiro instance;
    [SerializeField] private float dinheiro = 1000.00f;
    [SerializeField] GameObject gameOverPanel;
    //[SerializeField] GameObject ContasPanel;
    public bool travarCompra= false;
    public readonly float LIMITE = -1000f;
    public int diaGameOver;
    
    private void Awake()
    {
        instance = this;
        
    }
    void Start()
    {
        UIDinheiro.instance.UpdateUI(dinheiro);
    }

    public float GetMoney()
    {
        return dinheiro;
    }

    public bool MoneyController(float price)
    {
        return dinheiro - price >= LIMITE; 
    }

    public void DecreaseMoney(float price)
    {
        if (MoneyController(price))
        {
            dinheiro -= price;
            UIDinheiro.instance.UpdateUI(dinheiro);
        }
    }

    public void AddMoney(float price)
    {

        dinheiro += price;
        UIDinheiro.instance.UpdateUI(dinheiro);
        
    }

    public void Comprar(ObjectScene obj)
    {
        if(travarCompra== false)
        {
            if (MoneyController(obj.price))
            {
                travarCompra = true;
                DecreaseMoney(obj.price);
                GameObject sceneOBJ =Instantiate(obj.prefab, transform.position, transform.rotation);
                //obj.rotateParticle = Instantiate(ParticleController.Instance().rotateParticle, raycast.instance.obijeto.transform.position + new Vector3(1f, 2f, 0), raycast.instance.obijeto.transform.rotation);
                //obj.sellParticle = Instantiate(ParticleController.Instance().sellParticle, raycast.instance.obijeto.transform.position + new Vector3(-1f, 2f, 0), raycast.instance.obijeto.transform.rotation);
                //obj.rotateParticle.transform.parent = sceneOBJ.transform;
                //obj.sellParticle.transform.parent = sceneOBJ.transform;
                EstatisticaController.Instance().AddDespesas(obj.price);
            }
            else
            {
                //Passar na tela que o jogador Faliu.
            }

        }

    }

    public void Vender(GameObject obj)
    {
        ObjectScene objs = obj.GetComponent<ObjectData>().data;

        CameraController2.instance.lastClickedObject = null;
        AddMoney((int)(objs.price * 0.8f));
        EstatisticaController.Instance().DecreaseDespesas((int)(objs.price * 0.8f));

        if (objs.nome == "cadeira")
        {
            Debug.Log("vendeuCaeira");
            obj.GetComponentInParent <cadeira>().vendida = true;          
            listas.instance.RemoveDaLista(obj, listas.instance.cadeiras);
        }
        else if(obj.CompareTag("mesa"))
        {

        listas.instance.RemoveDaLista(obj, listas.instance.mesa);
        }
        Destroy(obj);       
    }

    public void ComprarComida(DistribuidoraOBS distribuidora, bool v)
    {
        if(v == true)
        {
            DecreaseMoney(distribuidora.price);
        }
        
    }

    public void SalarioFuncionario(float valor)
    {
        DecreaseMoney((int)valor);
        
    }
    public void GameOver()
    {

        if (diaGameOver < 3 && dinheiro <= 0)
        {
            diaGameOver++;
        }
        if (diaGameOver >=3 && dinheiro<=0)
        {
            diaGameOver = 0;
            gameOverPanel.SetActive(true);

        }
        if(dinheiro>0)
        {
            diaGameOver = 0;
        }
    }

    

}
