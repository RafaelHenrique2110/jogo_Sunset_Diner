using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StressTiredness : MonoBehaviour
{
    [SerializeField] protected OBJS_Contrato funcionario;
    [SerializeField][Range(0,5)] protected float tiredness;

    
}

public class SalarioFuncionario : StressTiredness
{
    float timeSalario = 0f;
    float timeSalarioM = 0f;
    bool timeSalarioBool = true;
    float salario;

    float status = 1;
    bool oneHour = false;

    public void FixedUpdate()
    {
        SalarioAtendente();

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

    public void Tiredness()
    {
        if (TimeController.instance.CurrentTime.Hour > TimeController.instance.startHour && TimeController.instance.CurrentTime.Hour < TimeController.instance.sunsetHour && oneHour == true)
        {
            tiredness += tiredness * status;
        }
        if(TimeController.instance.CurrentTime.Minute == 0)
        {
            oneHour = true;
        }


        if (timeSalario > TimeController.instance.sunsetHour + 0.2)
        {
            tiredness = 0;
        }
        
    }

}
