using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TecladoController : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            //ParticleController.Instance().rotateParticle.SetActive(true);
            Rotate();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Vender();
        }
    }
    void Rotate()
    {
        if (raycast.instance.obijeto != null)
        {
          raycast.instance.obijeto.transform.Rotate(0, 90, 0);
        }
    }
    void Vender()
    {
        if (raycast.instance.obijeto != null)
        {
            raycast.instance.obijeto.GetComponent<ButtonControlle>().Vender(raycast.instance.obijeto);
        }
        
    }
    
}
