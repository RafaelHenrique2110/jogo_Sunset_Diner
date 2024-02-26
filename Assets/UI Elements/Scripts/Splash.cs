using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour

{
    public float time;
    public string scene;
    void Start()
    {
        Invoke(nameof(PlayGame),time);
    }
    void PlayGame()
    {
        SceneManager.LoadScene(scene);
    }
} 
