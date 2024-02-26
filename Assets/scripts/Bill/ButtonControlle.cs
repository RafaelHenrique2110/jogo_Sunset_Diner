using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonControlle : MonoBehaviour
{

    public void Comprar(ObjectScene obj)
    {
        SistemaFinanceiro.instance.Comprar(obj);  
    }

    public void Vender(GameObject obj)
    {
        SistemaFinanceiro.instance.Vender(obj);
    }

    public void DoubleTime()
    {
       TimeController.instance.SetTimeMultiplier(2f);
       PauseManager.instance.currentTime = 2f;
    }

    public void StandardTime()
    {
       TimeController.instance.SetTimeMultiplier(1f);
       PauseManager.instance.currentTime = 1f;
    }

    public void Pause()
    {
        TimeController.instance.SetTimeMultiplier(0f);
    }

    public void ChangeView(Button button)
    {
        //CameraController2.instance.Isometric();
        //StartCoroutine(DisableButton(button));
        if (CameraController2.instance.isometric)
        {
            //CameraController2.instance.TopDown();
            CameraController2.instance.Isometric();
            CameraController2.instance.isometric = false;
            CameraController2.instance.MudaCorCorGrid(true);
            ParticleController.Instance().rotateParticle.SetActive(true);
            ParticleController.Instance().sellParticle.SetActive(true);
            UIManager.instance.animCD(true);
            //UIManager.instance.AtivaCanvas();
        }
        else
        {
            CameraController2.instance.Isometric();
            CameraController2.instance.isometric = true;
            CameraController2.instance.MudaCorCorGrid(false);
            ParticleController.Instance().rotateParticle.SetActive(false);
            ParticleController.Instance().sellParticle.SetActive(false);
            UIManager.instance.animCD(false);
            //UIManager.instance.DesativaCanvas();
        }
       
    }

    public void BtnEstoque(GameObject PainelEstoque)
    {
        AlertManager.instance.ClearAlert();
        AlertManager.instance.ClearGameAlert();
        if (PainelEstoque.activeSelf)
        {
          
            PainelEstoque.SetActive(false);
            NotificationController.instance.diaContas = 0;
        }
        else
        {
            //AlertManager.instance.SetAlert("");
            PainelEstoque.SetActive(true);
        }
    }

    public void PlayAnimationHUD(Animator anim)
    {
        anim.Play("HudUp");
        anim.gameObject.SetActive(true);
        //Debug.Log("chamei anim HUD");
    }

    

    public void PlayAnimationHUD2(Animator anim2)
    {
        anim2.Play("HudDown");
        //anim2.gameObject.SetActive(false);
    }

    public void BtnContrato(GameObject PainelContrato)
    {
        if (PainelContrato.activeSelf)
        {
            PainelContrato.SetActive(false);
        }
        else
        {
            AlertManager.instance.setas[0].SetActive(false);
            AlertManager.instance.SetAlert("Clique em 'Contratar' para escolher uma Atendente");
            PainelContrato.SetActive(true);
            AlertManager.instance.SetGameAlert("Você pode sortear uma nova Atendente por $100");
        }
    }

    IEnumerator DisableButton(Button button)
    {
        button.interactable = false;
        yield return new WaitForSeconds(1f);
        button.interactable = true;
    }

    public void BtnPassarTempoNoite(GameObject button)
    {
       PauseManager.instance.UnPauseGame();
        button.SetActive(false);
    }

    public void BTNNovoDia()
    {
        BTNNovoDia(listas.instance.canvas);
        
    }
    
    private void BTNNovoDia(List<GameObject> canvas)
    {
        canvas[0].GetComponent<Animator>().Play("CanvasSumir");
        //canvas[0].SetActive(false);
        canvas[1].SetActive(true);
        Time.timeScale = 1;
        AlertManager.instance.ClearAlert();
        AlertManager.instance.ClearGameAlert();
        EstatisticaController.Instance().dia++;
        EstatisticaController.Instance().DiaContador();
    }

    public void BuyCard(ItemUI cardCorreposdente)
    {

        if (SistemaFinanceiro.instance.MoneyController(cardCorreposdente.cardPrice))
        {
           transform.parent.gameObject.SetActive(false);
            SistemaFinanceiro.instance.DecreaseMoney(cardCorreposdente.cardPrice);
        }
        else
        {
            //gameObject.GetComponent<Button>().interactable = false;
        }
    }

    public void DesblockAtendente(ContratoUIAtendente cardCorreposdente)
    {
        
        //UpgradeController.Instance().DesblockCardAtendente(cardCorreposdente);

        if (SistemaFinanceiro.instance.MoneyController(cardCorreposdente.priceLock))
        {
            transform.parent.gameObject.SetActive(false);
            SistemaFinanceiro.instance.DecreaseMoney(cardCorreposdente.priceLock);
        }
        else
        {
            //gameObject.GetComponent<Button>().interactable = false;
        }
        
    }

    public void DesblockCozinheiro(ContratoUI cardCorreposdente)
    {
        if (SistemaFinanceiro.instance.MoneyController(cardCorreposdente.priceLock))
        {
            transform.parent.gameObject.SetActive(false);
            SistemaFinanceiro.instance.DecreaseMoney(cardCorreposdente.priceLock);
        }
        else
        {
            //gameObject.GetComponent<Button>().interactable = false;
        }
    }

    public void TakeLoan(Button b)
    {
        b.interactable = false;
        BankController.Instance().TakeLoan(b);
    }

    public void PayLoan()
    {
        BankController.Instance().PayLoan();
    }
    int valor2;
    public void NovoDia()
    {
        if (valor2 == 0)
        {
            AlertManager.instance.setas[5].SetActive(true);
            AlertManager.instance.SetAlert("Pronto! Agora podemos iniciar um Novo Dia!");
            valor2++;
        }
        else
        {

        }
    }

    int valor;
    public void FecharCanvasInfo(GameObject canvas)
    {
        
        if(valor == 0)
        {
            canvas.SetActive(true);
            valor++;
        }
        else
        {
            canvas.SetActive(false);
        }
    }
}
