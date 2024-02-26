using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class botaoObj : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject obt;
    public Transform gridToRotate;
    private obj objeto;

    private void Start()
    {
        objeto = GetComponent<obj>();
    }
    // Update is called once per frame
    public void Rotacao()
    {
        gridToRotate.Rotate(0, 90, 0);

    }
    private void Update()
    {
        if(!objeto.seguirMouse)
            obt.transform.rotation = gridToRotate.transform.rotation;
    }
    public void Mover()
    {
        if (obt.GetComponent<obj>().ativaBotao == true)
        {
            obt.GetComponent<obj>().seguirMouse = true;
        }
    }
}