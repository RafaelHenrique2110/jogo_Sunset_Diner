using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    public static GameController control;

   
    public bool isPause = false;
  
    public Text txtObjetivo;
  
    
    private string _myFirstScene = "SecondStage";
    void Start()
    {
       //Cursor.visible = true;
        control = this;
        Invoke(nameof(RemoveObjective),3f);
    }

    public void RemoveObjective()
    {
        control.txtObjetivo.text =("");
    }
    public void SetPause(bool p)
    {
        isPause = p;
       
        if (isPause)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}
