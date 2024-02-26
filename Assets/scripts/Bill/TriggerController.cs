using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerController : MonoBehaviour
{
    [SerializeField] GameObject anim;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Vehicle"))
        {
            Debug.Log("chamando anim");
            anim.GetComponent<Animation>().Play();
        }
    }
}
