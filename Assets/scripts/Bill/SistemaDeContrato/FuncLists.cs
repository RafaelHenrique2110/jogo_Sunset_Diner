using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuncLists : MonoBehaviour
{
    private static FuncLists instance;
    public List<OBJS_Contrato> cozinheirosOBJ = new List<OBJS_Contrato>();
    public List<ContratoUI> cozinheirosCards = new List<ContratoUI>();
    public List<OBJS_Contrato> atendentesOBJ = new List<OBJS_Contrato>();
    public List<ContratoUIAtendente> atendentesCards = new List<ContratoUIAtendente>();

    private void Awake()
    {
        instance = this;
    }

    public static FuncLists Instance()
    {
        return instance;
    }
}
