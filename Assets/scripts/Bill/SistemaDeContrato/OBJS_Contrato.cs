using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "ObjectScene", menuName = "ScriptableObjects/Contratos", order = 1)]
public class OBJS_Contrato : ScriptableObject
{
    
    public GameObject model;
    public Sprite imagem;
    public string nome;
    public string descricao;
    public float salario;
    [Range(0, 5)] public float qualidade;
    public List<DistribuidoraOBS> distribuidora;
}
