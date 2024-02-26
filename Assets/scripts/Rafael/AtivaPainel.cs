
using UnityEngine;

public class AtivaPainel : MonoBehaviour
{
    
    public GameObject paineilAtivar;
    public GameObject[] paineilDesativar;
    public void AtivarPainel()
    {
        UIManager.instance.ActivePainel(this);
    }

}
