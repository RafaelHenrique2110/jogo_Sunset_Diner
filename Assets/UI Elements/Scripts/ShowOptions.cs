using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

public class ShowOptions : MonoBehaviour
{
    public Button yourButton;
    public GameObject panel;
    public Slider sldMusicMenu;
    public Slider sldFXMenu;
    private float _fxMenu;
    private float _musicMenu;
    public AudioSource optionSound;
    void Start()
    {
        // Cursor.visible = true;
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        optionSound.Play();
        //Cursor.visible = true;
        if (panel.activeSelf)
        {
            optionSound.Play();
            panel.SetActive(false);
        }
        else
        {
            optionSound.Play();
            panel.SetActive(true);
        }
        optionSound.Play();
    }
}