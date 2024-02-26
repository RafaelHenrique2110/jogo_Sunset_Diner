using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycast : MonoBehaviour
{
    public static raycast instance;
    public float DistanciaPegarObjeto;
    public float DistanciaMaxObjeto;
    public float DistanciaMinObjeto;
    public Transform cub;
    public Vector3 PosObjeto;
    public GameObject obijeto;
    public GameObject objetoSelecionado;

    public float speed = 0;


    void Start()
    {
        instance = this;
        DistanciaPegarObjeto = DistanciaMaxObjeto;
    }


    void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward * DistanciaPegarObjeto, Color.green);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            PosObjeto = new Vector3(RoundToHalf(hit.point.x), 0f, RoundToHalf(hit.point.z));
            if(hit.collider.gameObject.CompareTag("mesa")|| hit.collider.gameObject.CompareTag("cadeira"))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    //objetoSelecionado = hit.collider.gameObject;
                    //hit.collider.transform.GetChild(0).gameObject.SetActive(true);
                    //hit.collider.transform.GetChild(1).gameObject.SetActive(true);
                    //Debug.Log(hit.collider.transform.GetChild(0).gameObject.name);

                    obijeto = hit.collider.gameObject;

                    //if (objetoSelecionado != obijeto && obijeto != null && objetoSelecionado != null)
                    //{
                        //obijeto.transform.GetChild(0).gameObject.SetActive(false);
                        //obijeto.transform.GetChild(1).gameObject.SetActive(false);
                        //obijeto = objetoSelecionado;
                    //}
                }
                
                
            }
        }

        Move();
    }


    float RoundToHalf(float value)
    {
        float returnValue;

        float savedValue = value;
        float maxValue, minValue;


        value = Mathf.RoundToInt(value);

        if(value>savedValue)
        {
            maxValue = value;
            minValue = maxValue-0.5f;
        }
        else
        {
            minValue = value;
            maxValue = minValue + 0.5f;
        }


        if(savedValue - minValue > maxValue - savedValue)
        {
            returnValue = maxValue;
        }
        else
        {
            returnValue = minValue;
        }


        return returnValue;
    }

    void Move()
    {

        Vector3 mousePos = Input.mousePosition;

        transform.position = transform.position + mousePos * speed * Time.deltaTime;

    }
}
