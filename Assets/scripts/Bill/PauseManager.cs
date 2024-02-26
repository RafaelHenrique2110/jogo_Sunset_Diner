using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public static PauseManager instance;
    public GameStates gameState = GameStates.Resumed;
    public float currentTime=1;
    public float myTimeScale=1;
    public enum GameStates
    {
        Resumed,
        Paused,
        EditMode

    }
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
        /*
        if(Input.GetKeyDown(KeyCode.Tab) && gameState != GameStates.Paused)
        {
            ToggleEditMode();
        }
        */
    }
    public void TogglePause()
    {
        if(gameState != GameStates.EditMode)
        {
            if (gameState == GameStates.Resumed)
            {
                gameState = GameStates.Paused;
                myTimeScale = 0;
            }
            else
                UnPauseGame();
        }

    }
    public void ToggleEditMode()
    {
        if(gameState != GameStates.Paused)
        {
            if (gameState == GameStates.Resumed)
            {
                gameState = GameStates.EditMode;
                myTimeScale = 0;
                //UIManager.instance.sideMenu.SetActive(true);
                UIManager.instance.anim.Play("EditModeOn");

            }
            else
            {
                //UIManager.instance.sideMenu.SetActive(false);
                UIManager.instance.anim.Play("EditModeOFF");
                UnPauseGame();
            }
        }

    }
    public void UnPauseGame()
    {
        gameState = GameStates.Resumed;
        myTimeScale = currentTime;
    }
    
}
