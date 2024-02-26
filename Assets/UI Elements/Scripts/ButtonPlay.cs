using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ButtonPlay : MonoBehaviour
{
	public string cena;
    public Button yourButton;
    public AudioSource cliqueSom;
    void Start ()
	{
		Cursor.visible = true;
		Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

    private void Update()
    {
	    Cursor.visible = true;
    }

    void TaskOnClick()
	{
		cliqueSom.Play();
		SceneManager.LoadScene(cena);
	}
}