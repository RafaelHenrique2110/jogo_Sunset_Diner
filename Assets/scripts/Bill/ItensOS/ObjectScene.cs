using UnityEngine;

[CreateAssetMenu(fileName = "ObjectScene", menuName = "ScriptableObjects/Mobilha", order = 1)]
public class ObjectScene : ScriptableObject
{

    public GameObject prefab;
    public float price;
    public string nome;
    public GameObject rotateParticle;
    public GameObject sellParticle;
   
}
    