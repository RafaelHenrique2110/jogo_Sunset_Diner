using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SistemaContrato : MonoBehaviour
{
    private static SistemaContrato instance;

    float timeSalario = 0f;
    float timeSalarioM = 0f;
    bool timeSalarioBool = true;
    float salario;
    [SerializeField] private Text quantidadeFunc; //deletar
    public int quantidadeFuncAtual = 0; //deletar
    public int quantidadeCozAtual = 0; //deletar
    public int quantidadeAtendenteMax = 3;//deletar
    public int quantidadeCozinheiroMax = 3;//deletar

    public int IdAtendenteLista;
    int idCozinheiroLista;

    public GameObject gameController;

    public Transform pos;

    void Awake()
    {
        instance = this;
    }

    void FixedUpdate()
    {
        //SalarioAtendente();
    }

    public static SistemaContrato Instance()
    {
        return instance;
    }

    public void SalarioAtendente()
    {
        timeSalarioM = (TimeController.instance.CurrentTime.Minute);
        timeSalario = (TimeController.instance.CurrentTime.Hour + timeSalarioM / 100);
        if (timeSalario > TimeController.instance.sunsetHour - 0.1 && timeSalario < TimeController.instance.sunsetHour + 0.1 && timeSalarioBool)
        {
            Debug.Log("chamou if");
            salario = ContratoUI.Instance().funcionario.salario;
            SistemaFinanceiro.instance.SalarioFuncionario(salario);
            timeSalarioBool = false;
        }
        else if (timeSalario > TimeController.instance.sunsetHour + 0.2)
        {
            timeSalarioBool = true;
        }
    }

    public void PagarSalario(Image imagem, Transform pos)
    {
        StartCoroutine(ShowSalario(imagem, pos));
        SalarioAtendente();
    }

    IEnumerator ShowSalario(Image imagem, Transform pos)
    {
        Image.Instantiate(imagem, pos.position, pos.rotation);
        yield return new WaitForSeconds(5f);
        Image.Destroy(imagem);
    }

    public float QualityChange()
    {
        float quality = ContratoUI.Instance().funcionario.qualidade;

        if (quality <= 1)
        {
            salario += (float)(salario * 0.1);
        }
        if (quality <= 2)
        {
            salario += (float)(salario * 0.2);
        }
        if (quality <= 3)
        {
            salario += (float)(salario * 0.3);
        }
        if (quality <= 4)
        {
            salario += (float)(salario * 0.4);
        }
        if (quality <= 5)
        {
            salario += (float)(salario * 0.5);
        }

        return salario;
    }



    public void QuantidadeFunc(int max) //deletar
    {
        if (max == quantidadeAtendenteMax)
        {
            quantidadeFunc.text = quantidadeFuncAtual.ToString() + " / " + max;
            quantidadeFuncAtual++;
        }
        if (max == quantidadeCozinheiroMax)
        {
            quantidadeFunc.text = quantidadeFuncAtual.ToString() + " / " + max;
            quantidadeCozAtual++;
        }

    }

    /*public void IDAtendente()
    {
        //listas.instance.atendentes[IdAtendenteLista].GetComponent<Atendente>().id = listas.instance.atendentes.Count -1;
        //IdAtendenteLista++;
    }

    /*public void IDCozinheiro()
    {
        listas.instance.cozinheros[idCozinheiroLista].GetComponent<Cozinhero>().idAtendente = listas.instance.cozinheros.Count - 1;
        idCozinheiroLista++;
    }*/

}
