using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectScene", menuName = "ScriptableObjects/Comida", order = 3)]
public class ComidasOBS : ScriptableObject
{
    public int id;
    public string nome;
    public string tag;
    public Sprite imagem;
    public GameObject prefab;
    public float valor;
    //public IventarioIMGS slotImage;
    //public int quantidadeInventario;
    // 0 ao 14 comida quente / 15 ao 30 comida fria
}
