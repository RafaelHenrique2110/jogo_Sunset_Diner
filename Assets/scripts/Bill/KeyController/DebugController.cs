using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugController : MonoBehaviour
{
    bool showConsole;
    string input;

    public static DebugCommand Add_Money;
    public List<object> commandList;

    
    public void OnToggleDebug(InputValue value) //
    {
        showConsole = !showConsole;
    }

    public void OnReturn(InputValue value) //
    {
        if (showConsole)
        {
            handleInput();
            input = "";
        }
    }

    private void Awake()
    {
        Add_Money = new DebugCommand("add_money", "add 100000 money.", "add_money", ()=>
        {
            SistemaFinanceiro.instance.AddMoney(100000);
            UIDinheiro.instance.UpdateUI(SistemaFinanceiro.instance.GetMoney());
            //Debug.Log("cheat");
        });

        commandList = new List<object>
        {
            Add_Money
        };
        
    }

    private void OnGUI()
    {
        if (!showConsole) { return; }

        float y = Screen.height - 30; // mudar a altura do console

        GUI.Box(new Rect(0, y, Screen.width, 30), "");
        GUI.backgroundColor = new Color(0,0,0,0);
        input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), input);
    }

    private void handleInput()
    {
        for (int i = 0; i < commandList.Count; i++)
        {
            DebugComandBase comandBase = commandList[i] as DebugComandBase;

            if (input.Contains(comandBase.commandID))
            {
                if(commandList[i] as DebugCommand != null)
                {
                    (commandList[i] as DebugCommand).Invoke();
                }
            }
        }
            
    }
}
