using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoardUI : MonoBehaviour
{
    Transform camTransform;

    Quaternion originalRotation;

    private void Start()
    {
        camTransform = Camera.main.transform;
        originalRotation = transform.rotation;
    }

    private void Update()
    {
        //transform.rotation = camTransform.rotation * originalRotation;
        transform.LookAt(new Vector3(camTransform.position.x, camTransform.position.y, camTransform.position.z));
    }
}
