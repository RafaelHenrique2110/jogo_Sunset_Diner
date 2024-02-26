using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Atendente : MonoBehaviour
{

    public static Atendente instance;
    public enum Estados { LAVARVAZILHA, DARPEDIDOCOZINHEIRO, INDOLIMPARMESA, PEGARPEDIDO, COLOCARPEDIDOBANCADA, IRPARABANCADA, IRAPARACLIENTE, ENTREGARPEDIDO, ESPERA, DARPEDIDOCLIENTE, START, ESPERAPEDIDO, IRENTREGARPEDIDO };
    public int tipoCliente = 1, id = 0, idAlimendoLimpar;
    public float valorPagar;
    public int pedidoRecebido;
    public Vector3 dir;
    public bool escolherCliente = true;
    public bool estouOcupado = false;
    public GameObject mao, vazilha;
    public GameObject cliente;
    public GameObject PedidoDoCliente;
    public GameObject posiESpera;
    public Estados current_state;
    public Estados novoEstado;
    public int posiBancada = 0;
    public bool ativaPosi = true;
    public Transform target;
    public NavMeshAgent agent;
    public int espera;
    public int tipoMesa;
    public float salario;
    float timeSalario = 0f;
    float timeSalarioM = 0f;
    bool timeSalarioBool = true;
    public bool canChangeState = false;
    float currentSpeed;
    public int escolherCozinhero = 0;
    public ComidasOBS meuPedidoDistribuidora;
    public DistribuidoraOBS distribuidoraDoCliente;
    public int quantidadeDeCozinerosNaoTemPedido = 0;
    public GameObject cozinheroAnterior;
    public GameObject meuCozinheiro;
    public Animator anim;
    bool recebeuSalario;
    public int diasSemPagar;
    public int distribuidora_analizada_cozinhero;
    public GameObject salarioVFX;
    void Start()
    {
        currentSpeed = agent.speed;
        instance = this;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (PauseManager.instance.gameState == PauseManager.GameStates.Resumed)
        {
            agent.speed = currentSpeed * PauseManager.instance.myTimeScale;
            anim.speed = 1f;

            if (current_state != novoEstado)
            {
                current_state = novoEstado;

            }

            if (agent.remainingDistance < 3f)
            {
                ChangeState();

            }
        }
        else if (agent.speed > 0)
        {
            agent.speed = 0;
            anim.speed = 0;
        }

    }


    //private void Update()
    //{
    //SalarioAtendente();

    //}

    public void SalarioAtendente()
    {

    }
    public void salarioChecker()
    {

        Debug.Log("salarioChecker()");
        if (SistemaFinanceiro.instance.GetMoney() >= salario)
        {
            SistemaFinanceiro.instance.SalarioFuncionario(salario);
            recebeuSalario = true;
            diasSemPagar = 0;
        }
        if (SistemaFinanceiro.instance.GetMoney() < salario)
        {
            Debug.Log("diasSemPagar++");
            diasSemPagar++;
        }
        if (diasSemPagar >= 2)
        {
            Demition();
            diasSemPagar = 0;
        }
    }

    public void Demition()
    {
        AlertManager.instance.SetGameAlert("Funcionários Demitidos");
        Destroy(gameObject);
        //listas.instance.atendentes[id] = null;
        FuncLists.Instance().atendentesCards[id].GetComponent<ContratoUIAtendente>().contratado = false;
        FuncLists.Instance().cozinheirosCards[id].GetComponent<ContratoUIAtendente>().ChangeButton(true);
    }
    void ChangeState()
    {
        if (current_state == Estados.START)
        {
            PosiEspera();
        }
        if (current_state == Estados.ESPERA)
        {
            CheckCliente();
        }
        else if (current_state == Estados.IRAPARACLIENTE)
        {
            PegarPedido();
        }
        else if (current_state == Estados.PEGARPEDIDO)
        {
            IrAteaBancada();
        }
        else if (current_state == Estados.IRPARABANCADA)
        {
            SorteiaCozinheiro();

        }

        if (current_state == Estados.IRENTREGARPEDIDO)
        {
            EntregarPedido();
        }

        else if (current_state == Estados.ENTREGARPEDIDO)
        {
            DarPedidoCliente();
        }

        if (current_state == Estados.INDOLIMPARMESA)
        {
            canChangeState = false;
            LevarVazilhaSuja();
        }
        if (current_state == Estados.LAVARVAZILHA)
        {
            ColocarVazilhaSujaBancada();
        }
        if (current_state == Estados.DARPEDIDOCOZINHEIRO)
        {
            ColocarPedidoNaBancada();
        }


    }
    void CheckLimparMesa()
    {

        // LimparMesa();

    }

    void PosiEspera()
    {
        anim.SetBool("isIdle", true);
        espera = id;
        
            posiESpera = Espera.instance.esperaposi[espera].gameObject;

            //Debug.Log("<color=yellow>Esperar</color>");
            target = posiESpera.transform;
            Move();

            quantidadeDeCozinerosNaoTemPedido = 0;
            cozinheroAnterior = null;
            posiESpera.GetComponent<posiEspera>().ocupado = true;
            novoEstado = Estados.ESPERA;

    }
    void CheckCliente()
    {
        if (listas.instance.cliente.Count > 0)
        {
            tipoMesa = Random.Range(0, listas.instance.mesa.Count);
            if (listas.instance.mesa.Count > 0 && listas.instance.mesa[tipoMesa].GetComponent<mesa>().clientesNaMesa.Count > 0)
            {
                tipoCliente = Random.Range(0, listas.instance.mesa[tipoMesa].GetComponent<mesa>().clientesNaMesa.Count);
                if (listas.instance.mesa[tipoMesa].GetComponent<mesa>().clientesNaMesa[tipoCliente].GetComponent<cliente>().esperandoSerAtendido && estouOcupado == false)
                {

                    cliente = listas.instance.mesa[tipoMesa].GetComponent<mesa>().clientesNaMesa[tipoCliente];
                    posiESpera.GetComponent<posiEspera>().ocupado = false;
                    // Debug.Log("<color=yellow>Atender</color>");
                    target = listas.instance.mesa[tipoMesa].transform;
                    listas.instance.mesa[tipoMesa].GetComponent<mesa>().clientesNaMesa[tipoCliente].GetComponent<cliente>().esperandoSerAtendido = false;
                    listas.instance.clienteEsperando.Remove(cliente);
                    Move();
                    estouOcupado = true;
                    posiESpera.GetComponent<posiEspera>().ocupado = false;
                    novoEstado = Estados.IRAPARACLIENTE;
                }
                else if (listas.instance.mesa[tipoMesa].GetComponent<mesa>().clientesNaMesa[tipoCliente].GetComponent<cliente>().esperandoSerAtendido == false)
                {
                    tipoCliente = Random.Range(0, listas.instance.mesa[tipoMesa].GetComponent<mesa>().clientesNaMesa.Count);
                }
                else
                {
                    ////////////////////
                    novoEstado = Estados.START;
                }
            }
            else
            {
                tipoMesa = Random.Range(0, listas.instance.mesa.Count);
            }


        }

    }

    public int clienteAtendido;
    public cliente meucliente;
    void PegarPedido()
    {
        meucliente = listas.instance.mesa[tipoMesa].GetComponent<mesa>().clientesNaMesa[tipoCliente].GetComponent<cliente>();
        posiESpera.GetComponent<posiEspera>().ocupado = false;
        estouOcupado = true;
        //Debug.Log("<color=yellow>Pegar Pedido</color>");

        meucliente.fuiAtendido = true;
        pedidoRecebido = meucliente.tipoPedido;

        distribuidoraDoCliente = meucliente.minhaDistribuidora;
        meuPedidoDistribuidora = meucliente.meuPedidoDistribuidora;

        clienteAtendido = tipoCliente;

        //pedido.numeroPedido = pedidoRecebido;
        //pedido.cliente = tipoCliente;
        novoEstado = Estados.PEGARPEDIDO;



    }
    void IrAteaBancada()
    {
        posiBancada = Random.Range(0, Bancada.instance.posicaoPegar.Count);
        if (Bancada.instance.posicaoPegar[posiBancada].GetComponent<DetectorPedidos>().escolido == false)
        {
            target = Bancada.instance.posicaoPegar[posiBancada].transform;
            Move();
            //Debug.Log("<color=yellow>Indo para bancada</color>");
            novoEstado = Estados.IRPARABANCADA;
            Bancada.instance.posicaoPegar[posiBancada].GetComponent<DetectorPedidos>().escolido = true;

        }
        else
        {
            posiBancada = Random.Range(0, Bancada.instance.posicaoPegar.Count);
        }

    }

    void SorteiaCozinheiro()  //Estados.COLOCARPEDIDOBANCADA
    {
        SorteiaCozinheiro(0);
    }
    void SorteiaCozinheiro(int n)
    {

        //if (quantidadeDeCozinerosNaoTemPedido == listas.instance.cozinheros.Count) 
        //{      
        //    meuCozinheiro.GetComponent<Cozinhero>().NaoTenhoIgrediente();
        // }
        // else
        // {
        novoEstado = Estados.DARPEDIDOCOZINHEIRO;
        //}

    }
    void ColocarPedidoNaBancada() // Estados.DARPEDIDOCOZINHEIRO
    {

        DetectorPedidos detectorPedido = Bancada.instance.posicaoPegar[posiBancada].GetComponent<DetectorPedidos>();
        detectorPedido.atendente = this.gameObject;
        detectorPedido.cliente = clienteAtendido;
        detectorPedido.pedido = pedidoRecebido;
        detectorPedido.ocupado = true;
        detectorPedido.idAtendente = id;
        detectorPedido.distribuidoraDoAtendente = distribuidoraDoCliente;
        Bancada.instance.posicaoPegar[posiBancada].GetComponent<DetectorPedidos>().posiBancadaAtendente = posiBancada;
        // Debug.Log("<color=yellow>Colocar Pedido na bancada</color>");
        //Debug.Log("<color=yellow>Esperando</color>");
        novoEstado = Estados.ESPERAPEDIDO;
    }

    public bool EntregarPedido() // Estados.IRENTREGARPEDIDO
    {
        Bancada.instance.posicaoPegar[posiBancada].GetComponent<DetectorPedidos>().pedidoPronto.GetComponent<alimento>().transform.position = mao.transform.transform.position;
        Bancada.instance.posicaoPegar[posiBancada].GetComponent<DetectorPedidos>().pedidoPronto.GetComponent<alimento>().transform.parent = mao.transform;
        PedidoDoCliente = Bancada.instance.posicaoPegar[posiBancada].GetComponent<DetectorPedidos>().pedidoPronto;
        tipoCliente = Bancada.instance.posicaoPegar[posiBancada].GetComponent<DetectorPedidos>().pedidoPronto.GetComponent<alimento>().cliente;
        valorPagar = meuPedidoDistribuidora.valor; //Bancada.instance.posicaoPegar[posiBancada].GetComponent<DetectorPedidos>().pedidoPronto.GetComponent<alimento>().valorComida;

        target = listas.instance.mesa[tipoMesa].transform;
        Move();
        novoEstado = Estados.ENTREGARPEDIDO;
        return false;
    }


    public void DarPedidoCliente()
    {
        if (listas.instance.mesa[tipoMesa].GetComponent<mesa>().clientesNaMesa.Count > 0)
        {
            // Debug.Log("PEDIDO ENTREGUE");
            Bancada.instance.posicaoPegar[posiBancada].GetComponent<DetectorPedidos>().pedidoPronto.GetComponent<alimento>().transform.position = cliente.GetComponent<cliente>().mao.transform.transform.position;
            Bancada.instance.posicaoPegar[posiBancada].GetComponent<DetectorPedidos>().pedidoPronto.GetComponent<alimento>().transform.parent = cliente.GetComponent<cliente>().mao.transform;
            SistemaFinanceiro.instance.AddMoney(valorPagar);
            UIDinheiro.instance.UpdateUI(SistemaFinanceiro.instance.GetMoney());
            cliente.GetComponent<cliente>().ativaTimeComer = true;
            cliente.GetComponent<cliente>().anim.SetBool("Eating", true);
            cliente.GetComponent<cliente>().particulas[2].SetActive(true);
            cliente.GetComponent<cliente>().fuiAtendido = false;
            cliente.GetComponent<cliente>().meuPedido = PedidoDoCliente;
            estouOcupado = false;
            if (listas.instance.clienteEsperando.Count > 0)
            {
                novoEstado = Estados.ESPERA;
            }
            else if (listas.instance.clienteEsperando.Count <= 0)
            {
                novoEstado = Estados.START;
            }

        }


    }
    public void LimparMesa(GameObject dir)
    {
        posiESpera.GetComponent<posiEspera>().ocupado = false;
        vazilha = dir;
        StartCoroutine(ChangeStateNumerator());
        Espera.instance.esperaposi[espera].GetComponent<posiEspera>().ocupado = false;
        target = dir.transform;
        Move();

        novoEstado = Estados.INDOLIMPARMESA;
        // Debug.Log("pegar lou�a suja");

    }
    void LevarVazilhaSuja()
    {
        // Debug.Log("Levando vasilha Suja " + agent.remainingDistance);
        Debug.Log(target.gameObject.name);
        vazilha.GetComponent<alimento>().recolido = true;
        estouOcupado = true;
        vazilha.transform.position = mao.transform.transform.position;
        vazilha.transform.parent = mao.transform;
        posiBancada = Random.Range(0, Bancada.instance.posicaoPegar.Count);
        if (Bancada.instance.posicaoPegar[posiBancada].GetComponent<DetectorPedidos>().escolido == false)
        {
            target = Bancada.instance.posicaoPegar[posiBancada].transform;
            Move();
            novoEstado = Estados.LAVARVAZILHA;
            Bancada.instance.posicaoPegar[posiBancada].GetComponent<DetectorPedidos>().escolido = true;
        }
        else
        {
            posiBancada = Random.Range(0, Bancada.instance.posicaoPegar.Count);
        }




    }
    void ColocarVazilhaSujaBancada()
    {
        estouOcupado = false;
        vazilha.transform.position = listas.instance.bancada[0].GetComponent<Bancada>().posiBancada.transform.position;
        vazilha.transform.parent = null;
        listas.instance.mesaSuja.Remove(vazilha);
        Bancada.instance.posicaoPegar[posiBancada].GetComponent<DetectorPedidos>().escolido = false;
        if (listas.instance.clienteEsperando.Count > 0)
        {
            novoEstado = Estados.ESPERA;
        }
        else if (listas.instance.clienteEsperando.Count <= 0)
        {
            novoEstado = Estados.START;
        }
    }


    void Move()
    {
        agent.SetDestination(target.position);
        anim.SetBool("isIdle", false);

    }

    IEnumerator ChangeStateNumerator()
    {
        yield return new WaitForSeconds(0.2f);
        canChangeState = true;
    }
}
