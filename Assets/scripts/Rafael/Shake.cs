using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public bool shake;
    [Range(0, 1)]
    public float amplitude;

    void FixedUpdate()
    {
        Vector3 pos = transform.position;
        pos.x = transform.position.x + Mathf.PingPong(Time.time, amplitude)-amplitude*0.5f;
        transform.position = pos;
    }
}
