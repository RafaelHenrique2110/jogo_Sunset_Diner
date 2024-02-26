using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mesa : MonoBehaviour
{
    public static mesa instance;
    public List<GameObject> clientesNaMesa;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        listas.instance.mesa.Add(this.gameObject);
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0;i< clientesNaMesa.Count; i++)
        {
            if(clientesNaMesa[i].gameObject.GetComponent<cliente>().estouMesa == false)
            {
                //clientesNaMesa.RemoveAt(i);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
      
    }
   
}
