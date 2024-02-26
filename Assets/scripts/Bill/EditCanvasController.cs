using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.UI;

public class EditCanvasController : MonoBehaviour
{
    private static EditCanvasController instance;

    public List<Text> textos = new List<Text>();
    public List<ObjectScene> mobilhas = new List<ObjectScene>();
    private void Awake()
    {
        instance = this;
    }

    public static EditCanvasController Instance()
    {
        return instance;
    }

    private void Start()
    {
        StringBuilder texto = new StringBuilder("");
        StringBuilder texto2 = new StringBuilder("");
        StringBuilder texto3 = new StringBuilder("");
        StringBuilder texto4 = new StringBuilder("");
        texto.AppendFormat("{0:C} ", mobilhas[0].price);
        texto2.AppendFormat("{0:C} ", mobilhas[1].price);
        texto3.AppendFormat("{0:C} ", mobilhas[2].price);
        texto4.AppendFormat("{0:C} ", mobilhas[3].price);

        textos[0].text = texto.ToString();
        textos[1].text = texto2.ToString();
        textos[2].text = texto3.ToString();
        textos[3].text = texto4.ToString();

    }
}
