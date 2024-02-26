using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class obj : MonoBehaviour
{  
    public  bool seguirMouse= true;
    public  bool ativaBotao= true;
    public List<GameObject> corGrid;

    public Transform transformToRotate;

    bool resetShake = true;
    public bool ativarParticula = true;

    
    // Update is called once per frame
    void Update()
    {    
        if(ativaBotao == true){
            if (Input.GetMouseButton(0)){
               
               seguirMouse = false;
                if (seguirMouse == false)
                {
                    
                   
                   SistemaFinanceiro.instance.travarCompra = false;
                }
                if (ativarParticula)
                {
                    

                    ativarParticula = false;
                }

            }
        }


        if (seguirMouse == true)
        {
            transform.position = raycast.instance.PosObjeto;
            SistemaFinanceiro.instance.travarCompra = true ;
            if (resetShake)
                StartCoroutine(ResetaShake());
        }
        
        
    }

    void AtivaShake()
    {
        //transform.position += new Vector3 (0, 1f, 0);
        transformToRotate.DOShakeRotation(1f, new Vector3(7, 0, 0.2f), 12, 0, false);
    }
    IEnumerator ResetaShake()
    {
        resetShake = false;
        AtivaShake();
        yield return new WaitForSeconds(1);
        resetShake = true;
    }
   
}
