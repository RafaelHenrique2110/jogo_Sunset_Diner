using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string cena;
    private Scene _scene;
    //public AudioSource cliqueSound;
    public void OnScene()
    {
       // Cursor.visible = true;
        if ( SceneManager.GetActiveScene().name !="OpenScene")
        {
            //cliqueSound.Play();
           
          //  Cursor.visible = true;
            //GameController.control.SetPause(false);
        }
        
        //cliqueSound.Play();
        SceneManager.LoadScene(cena);
    }
}

