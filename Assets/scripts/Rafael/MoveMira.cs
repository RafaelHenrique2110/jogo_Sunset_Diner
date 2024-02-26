using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMira : MonoBehaviour
{

    void Update()
    {
        transform.position = raycast.instance.PosObjeto; 
    }
}
