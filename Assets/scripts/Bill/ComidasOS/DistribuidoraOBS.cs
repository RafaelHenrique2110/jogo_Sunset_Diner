using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ObjectScene", menuName = "ScriptableObjects/ContratoComida", order = 2)]
public class DistribuidoraOBS : ScriptableObject
{
    public int id;
    public Sprite imagem;
    public float price;
    public string nome;
    public List<ComidasOBS> comidas;
    public List<byte> quantidadeComidas;
    public Material material;

    /*public void DistruibuidoraInventarioAumentar(IventarioIMGS slotImage)
    {
        for(int i = 0; i < comidas.Count; i++)
        {
            comidas[i].slotImage.AumentarQuantidade(quantidadeComidas[i]);
            //distribuidora.comidas[i].slotImage.AtualizaTexto();
        }
    }

    public void DistruibuidoraDiminuir(DistribuidoraOBS distribuidora, int comidaEscolhida)
    {   
        distribuidora.comidas[comidaEscolhida].slotImage.DiminuirQuantidade(quantidadeComidas[comidaEscolhida]);
    }*/
}
