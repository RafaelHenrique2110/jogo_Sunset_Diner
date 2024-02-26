using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public float DistanciaPegarObjeto;
   public float DistanciaMaxObjeto;
   public float DistanciaMinObjeto;
   public Vector3 PosObjeto;
   public float posiX=0f,posiY= 0f;
   public float speed =0;
   float posiZ = 0;
   void Start () {
      DistanciaPegarObjeto = DistanciaMaxObjeto;
   }
   

   void Update () {
      
    
      
      Move();
     
   }
   
   void Move(){
       
      Vector3 mousePos = Input.mousePosition;
      Vector3 scrow = new Vector3(0,Input.mouseScrollDelta.y,0);
      //Debug.Log(scrow);
      
        
     
      
      if (mousePos.x >=1920f){
         posiX=1f;
         posiY =0;
        
      
      }
       if (mousePos.x <0){
         
         posiX=-1f;;
         posiY =0;
         
      }
      if (mousePos.y >1080f){
         
         posiY=1f;
         posiX =0;
         posiZ= 0;
         
      }
      if (mousePos.y <0){
           posiY=-1f;
           posiX =0;
           posiZ= 0;
         
      }
      if( mousePos.x >0 && mousePos.x< 1920 ){
         speed =0f;
      }else{
         speed =1.0f;
      }
      if( mousePos.x >0 && mousePos.x< 1920 && mousePos.y >0 && mousePos.y< 1080 ){
         speed =0f;
      }else{
         speed =1.0f;
      }
      //if ( scrow.y == 1.0f){
       //   posiZ ++;
         // speed= 1.0f;
//}
     // if ( scrow.y == -1.0f){
          
         //   speed= -1.0f;
     // }
      
     
      Vector3 posi = new Vector3(posiX,posiZ,posiY);
      transform.position= transform.position + posi * speed *Time.deltaTime;
     
   }
      
}

