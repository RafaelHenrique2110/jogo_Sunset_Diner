using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AlertManager : MonoBehaviour
{
    
    public static AlertManager instance;

    public bool isWaitressReady = false;
    public bool isCookerReady = false;
    public bool isContractReady = false;

    public Button btnCooker;
    public Button btnStorage;
    public Button btnInventory;
    //public Button btnWaitress;
    public Button btnNewDay;
    public Button btnBank;
    public Button btnContas;
        
    public Text txtAlerta;
    public Text txtGamebox;
        
    public GameObject alertBox;
    public GameObject gameBox;

    public List<GameObject> setas = new List<GameObject>();
    //public GameObject character;

    //public Button Contrato;

    void Awake()
    {   
        SetAlert("Primeiro vamos contratar os funcionários!");
        //SetGameAlert("Primeiro vamos contratar os funcionários!");
        setas[0].SetActive(true);
        ClearGameAlert();
        
        // Verifica se ja existe uma instancia de AlertManager na cena
        if (instance != null)
        {
            Debug.LogError("Mais de uma instancia de AlertManager rodando!");
            return;
        }
        
        instance = this;
    }

    private void Update()
    {
        CheckButtons();
    }

    public void SetAlert(string alert)
    {
        alertBox.SetActive(true);
        txtAlerta.text = $"{alert}";
        Invoke(nameof(ClearAlert),3f);
    }

    public void ClearAlert()
    {
        txtAlerta.text = $"";
        alertBox.SetActive(false);
    }
    
    public void SetGameAlert(string alert)
    {
        gameBox.SetActive(true);
        txtGamebox.text = $"{alert}";
        Invoke(nameof(ClearGameAlert),3f);
    }
    
    public void ClearGameAlert()
    {
        txtGamebox.text = $"";
        gameBox.SetActive(false);
    }

    public void CheckButtons()
    {
        if (isCookerReady && isWaitressReady)
        {
            btnInventory.interactable = true;
            btnStorage.interactable = true;
            
        }
        else
        {
            btnInventory.interactable = false;
            btnStorage.interactable = false;
            
            //SetGameAlert("Necessário contratar Atendente e Cozinheiro!");
        }

        if (isWaitressReady)
        {
            btnCooker.interactable = true;
        }
        else
        {
            btnCooker.interactable = false;
            //SetGameAlert("Necessário contratar uma Atendente Antes!");
        }

        if (isContractReady && isCookerReady && isWaitressReady)
        {
            btnNewDay.interactable = true;
            btnBank.interactable = true;
            btnContas.interactable = true;
        }
        else
        {
            btnNewDay.interactable = false;
            btnBank.interactable = false;
            btnContas.interactable = false;
            //SetGameAlert("Necessário contratar Atendente, Cozinheiro e Fornecedor!");
        }
    }
}
