using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    public Button yourButton;
    public AudioSource quitSound;

    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        quitSound.Play();
        Application.Quit();
        Debug.Log("Quitting!");
    }
}