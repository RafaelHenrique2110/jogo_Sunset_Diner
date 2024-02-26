using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cozinhero : MonoBehaviour
{
    public static Cozinhero instance;
    public enum Estados { ESPERA, ANALIZAPEDIDO, IRPARABANCADA, COLOCARPEDIDONABANCADA, IRTRABALHAR };
    public int id;
    public Estados current_state;
    public Estados novoEstado;
    public int idPedido, idAtendente, posiBancadaAtendente;
    public int clientes;
    public float speed;
    public GameObject mao;
    public GameObject posiBancada;
    public GameObject atendente;
    public bool prepararOutroPedido = true, ocupado = false;
    public List<DistribuidoraOBS> distribuidora;
    public float salario;
    public GameObject pedidoPronto;
    public UnityEngine.AI.NavMeshAgent agent;
    public Transform target;
    public IventarioIMGS slotInventario;
    Vector3 dir;
    public int pendentRemove;
    public float queroAtender;
    public Animator anim;
    float currentSpeed;
    float timeSalario = 0f;
    float timeSalarioM = 0f;
    bool timeSalarioBool = true;
    public int posiEspera;

    public int diasSemPagar;
    bool recebeuSalario;

    public GameObject salarioVFX;
    void Start()
    {
        instance = this;
        currentSpeed = agent.speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (PauseManager.instance.gameState == PauseManager.GameStates.Resumed)
        {
            agent.speed = currentSpeed * PauseManager.instance.myTimeScale;
            anim.speed = 1f;
            queroAtender = (int)Mathf.PingPong(Time.time * Random.Range(0.1f, 0.5f), 1.1f);
            if (current_state != novoEstado)
            {
                current_state = novoEstado;

            }
            if (PauseManager.instance.gameState == PauseManager.GameStates.Resumed)
            {
                if (current_state == Estados.ESPERA)
                {
                    Espera();
                }
                if (agent.remainingDistance < 2f)
                {

                    if (current_state == Estados.ANALIZAPEDIDO)
                    {

                        AnalisePedido();
                        //distribuidora.DistruibuidoraDiminuir(distribuidora, numeroDoPedido);
                    }
                    if (current_state == Estados.IRPARABANCADA)
                    {
                        IrAteaBancada();
                    }
                    if (current_state == Estados.COLOCARPEDIDONABANCADA)
                    {
                        ColocarPedidoNaBancada();
                    }

                    if (current_state == Estados.IRTRABALHAR)
                    {
                        IRTRABALHAR();
                    }
                }

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
    //SalarioCozinheiro();
    //}
    public void SalarioCozinheiro()
    {
        if (TimeController.instance.CurrentTime.TimeOfDay.Hours == TimeController.instance.closeRestaurantTime && TimeController.instance.CurrentTime.TimeOfDay.Minutes == 3)
        {
            //salarioChecker();
        }
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
        Destroy(gameObject);
        //listas.instance.cozinheros[id] = null;
        FuncLists.Instance().cozinheirosCards[id].GetComponent<ContratoUI>().contratado = false;
        FuncLists.Instance().cozinheirosCards[id].GetComponent<ContratoUI>().ChangeButton(false);
    }

    void Espera()
    {
        if (agent.remainingDistance < 1f)
        {
            anim.SetBool("isIdle", true);
        }

        for (int i = 0; i < Bancada.instance.posicaoPegar.Count; i++)
        {
            if (Bancada.instance.posicaoPegar[i].GetComponent<DetectorPedidos>().ocupado && Bancada.instance.posicaoPegar[i].GetComponent<DetectorPedidos>().escolidoPeloCozinhero == false && queroAtender == 1f)
            {
                Bancada.instance.posicaoPegar[i].GetComponent<DetectorPedidos>().escolidoPeloCozinhero = true;
                posiBancadaAtendente = i;
                novoEstado = Estados.ANALIZAPEDIDO;
            }
        }
    }
    void AnalisePedido()
    {
        //anim.SetBool("isIdle", true);
        ocupado = true;


        atendente = Bancada.instance.posicaoPegar[posiBancadaAtendente].GetComponent<DetectorPedidos>().atendente;
        atendente.GetComponent<Atendente>().meuCozinheiro = this.gameObject;

        clientes = Bancada.instance.posicaoPegar[posiBancadaAtendente].GetComponent<DetectorPedidos>().cliente;
        idAtendente = Bancada.instance.posicaoPegar[posiBancadaAtendente].GetComponent<DetectorPedidos>().idAtendente;

        for (int i = 0; i < distribuidora.Count; i++)
        {
            if (distribuidora[i].id == atendente.GetComponent<Atendente>().distribuidoraDoCliente.id)
            {
                atendente.GetComponent<Atendente>().distribuidora_analizada_cozinhero = 0;
                if (prepararOutroPedido)
                {

                    idPedido = Bancada.instance.posicaoPegar[posiBancadaAtendente].GetComponent<DetectorPedidos>().pedido;
                    prepararOutroPedido = false;
                }
                slotInventario = Inventario.Instance().VerificarComida(distribuidora[i].comidas[idPedido]); // corrigir
                if (distribuidora[i].comidas[idPedido].tag == "forno" && slotInventario.Quantidade > 0)
                {
                    PrepararPedido(listas.instance.forno[0].transform, distribuidora[i].comidas[idPedido], slotInventario);

                }
                else if (distribuidora[i].comidas[idPedido].tag == "geladeira" && slotInventario.Quantidade > 0)
                {
                    PrepararPedido(listas.instance.geladeira[0].transform, distribuidora[i].comidas[idPedido], slotInventario);
                }
                else if (distribuidora[i].comidas[idPedido].tag == "armario" && slotInventario.Quantidade > 0)
                {
                    PrepararPedido(listas.instance.armario[0].transform, distribuidora[i].comidas[idPedido], slotInventario);
                }
                else
                {
                    NaoTenhoIgrediente();
                }
            }
            else
            {
                if (atendente.GetComponent<Atendente>().distribuidora_analizada_cozinhero >= distribuidora.Count)
                {
                    if (atendente.GetComponent<Atendente>().cozinheroAnterior != this.gameObject || atendente.GetComponent<Atendente>().cozinheroAnterior == null)
                    {
                        if (atendente.GetComponent<Atendente>().quantidadeDeCozinerosNaoTemPedido == listas.instance.cozinheros.Count)
                        {
                            atendente.GetComponent<Atendente>().meuCozinheiro.GetComponent<Cozinhero>().NaoTenhoIgrediente();
                        }
                        else
                        {
                            atendente.GetComponent<Atendente>().cozinheroAnterior = this.gameObject;

                            Bancada.instance.posicaoPegar[posiBancadaAtendente].GetComponent<DetectorPedidos>().escolidoPeloCozinhero = false;
                            novoEstado = Estados.ESPERA;
                            atendente.GetComponent<Atendente>().novoEstado = Atendente.Estados.IRPARABANCADA;
                            if (atendente.GetComponent<Atendente>().quantidadeDeCozinerosNaoTemPedido < SistemaContrato.Instance().quantidadeCozAtual)
                            {
                                atendente.GetComponent<Atendente>().quantidadeDeCozinerosNaoTemPedido++;
                            }
                            else if (atendente.GetComponent<Atendente>().quantidadeDeCozinerosNaoTemPedido >= SistemaContrato.Instance().quantidadeCozAtual)
                            {
                                NaoTenhoIgrediente();
                            }

                        }
                    }
                    else
                    {

                        Bancada.instance.posicaoPegar[posiBancadaAtendente].GetComponent<DetectorPedidos>().escolidoPeloCozinhero = false;
                        novoEstado = Estados.ESPERA;
                        atendente.GetComponent<Atendente>().novoEstado = Atendente.Estados.IRPARABANCADA;
                        if (atendente.GetComponent<Atendente>().quantidadeDeCozinerosNaoTemPedido >= SistemaContrato.Instance().quantidadeCozAtual)
                        {
                            NaoTenhoIgrediente();
                        }
                    }
                }
                else
                {
                    atendente.GetComponent<Atendente>().distribuidora_analizada_cozinhero++;
                    continue;
                }

            }
        }







    }


    void PrepararPedido(Transform localPreparo, ComidasOBS comida, IventarioIMGS slotInventario)
    {
        Bancada.instance.posiEsperaCozinhero[posiEspera].GetComponent<posiEspera>().ocupado = false;
        target = localPreparo.transform;
        Move();

        if (DectorDistancia())
        {
            posiBancadaAtendente = atendente.GetComponent<Atendente>().posiBancada;
            Inventario.Instance().UpdateUIConsumir(comida);
            pedidoPronto = Instantiate(comida.prefab, transform.position, transform.rotation);
            pedidoPronto.GetComponent<alimento>().cliente = clientes;
            pedidoPronto.transform.position = mao.transform.position;
            pedidoPronto.transform.parent = mao.transform;
            Bancada.instance.posicaoPegar[posiBancadaAtendente].GetComponent<DetectorPedidos>().pedidoPronto = pedidoPronto;
            //slotInventario.Quantidade--;
            Debug.Log("DIMINIR QUANTIDADE");
            novoEstado = Estados.IRPARABANCADA;
        }

    }
    bool DectorDistancia()
    {
        return agent.remainingDistance < 1f;
    }

    void IrAteaBancada()
    {
        posiEspera = Random.Range(0, Bancada.instance.posiEsperaCozinhero.Count);
        if (Bancada.instance.posiEsperaCozinhero[posiEspera].GetComponent<posiEspera>().ocupado == false)
        {
            Bancada.instance.posiEsperaCozinhero[posiEspera].GetComponent<posiEspera>().ocupado = true;
            target = Bancada.instance.posiEsperaCozinhero[posiEspera].transform;
            Move();
            Debug.Log("ESTOU INdO PARA BANCADA");
            novoEstado = Estados.COLOCARPEDIDONABANCADA;
        }
        else
        {
            posiEspera = Random.Range(0, Bancada.instance.posiEsperaCozinhero.Count);
        }
    }
    void ColocarPedidoNaBancada()
    {

        for (int j = 0; j < listas.instance.atendentes.Count; j++)
        {
            if (listas.instance.atendentes[j] == null) continue;
            if (listas.instance.atendentes[j].GetComponent<Atendente>().id == idAtendente)
            {

                pedidoPronto.transform.position = posiBancada.transform.position;
                pedidoPronto.transform.parent = posiBancada.transform;

                Debug.Log("O PEDIDO EST� PRONTO ");
                Bancada.instance.posicaoPegar[posiBancadaAtendente].GetComponent<DetectorPedidos>().ocupado = false;
                Bancada.instance.posicaoPegar[posiBancadaAtendente].GetComponent<DetectorPedidos>().escolido = false;

                novoEstado = Estados.ESPERA;
                ocupado = false;
                prepararOutroPedido = true;
                listas.instance.atendentes[j].GetComponent<Atendente>().novoEstado = Atendente.Estados.IRENTREGARPEDIDO;
                Bancada.instance.posicaoPegar[posiBancadaAtendente].GetComponent<DetectorPedidos>().escolidoPeloCozinhero = false;
            }

        }

    }
    public void NaoTenhoIgrediente()
    {
        EstatisticaController.Instance().ClientesNA();

        Debug.Log("nao tenho o igrediente");
        listas.instance.mesa[listas.instance.atendentes[idAtendente].GetComponent<Atendente>().tipoMesa].GetComponent<mesa>().clientesNaMesa[clientes].GetComponent<cliente>().novoEstado = cliente.Estados.IREMBORA;
        listas.instance.mesa[listas.instance.atendentes[idAtendente].GetComponent<Atendente>().tipoMesa].GetComponent<mesa>().clientesNaMesa[clientes].GetComponent<cliente>().particulas[1].SetActive(true);
        listas.instance.CadeirasOcupadas.Remove(listas.instance.mesa[listas.instance.atendentes[idAtendente].GetComponent<Atendente>().tipoMesa].GetComponent<mesa>().clientesNaMesa[clientes].GetComponent<cliente>().minhaCadeira);
        //listas.instance.mesa[listas.instance.atendentes[idAtendente].GetComponent<Atendente>().tipoMesa].GetComponent<mesa>().clientesNaMesa[clientes].GetComponent<cliente>().minhaCadeira.GetComponent<cadeira>().ocupado = false;
        if (listas.instance.clienteEsperando.Count > 0)
        {
            listas.instance.atendentes[idAtendente].GetComponent<Atendente>().novoEstado = Atendente.Estados.ESPERA;
        }
        else if (listas.instance.clienteEsperando.Count <= 0)
        {
            listas.instance.atendentes[idAtendente].GetComponent<Atendente>().novoEstado = Atendente.Estados.START;
        }
        listas.instance.atendentes[idAtendente].GetComponent<Atendente>().estouOcupado = false;
        Bancada.instance.posicaoPegar[posiBancadaAtendente].GetComponent<DetectorPedidos>().ocupado = false;
        Bancada.instance.posicaoPegar[posiBancadaAtendente].GetComponent<DetectorPedidos>().escolido = false;
        Bancada.instance.posicaoPegar[posiBancadaAtendente].GetComponent<DetectorPedidos>().escolidoPeloCozinhero = false;
        idPedido = -1;
        idAtendente = 0;
        posiBancadaAtendente = 0;
        clientes = 0;
        novoEstado = Estados.ESPERA;
        prepararOutroPedido = true;
        ocupado = false;



    }

    void IRTRABALHAR()
    {
        posiEspera = Random.Range(0, Bancada.instance.posiEsperaCozinhero.Count);
        if (Bancada.instance.posiEsperaCozinhero[posiEspera].GetComponent<posiEspera>().ocupado == false)
        {
            Bancada.instance.posiEsperaCozinhero[posiEspera].GetComponent<posiEspera>().ocupado = true;
            target = Bancada.instance.posiEsperaCozinhero[posiEspera].transform;
            Move();
            novoEstado = Estados.ESPERA;
        }
        else
        {
            posiEspera = Random.Range(0, Bancada.instance.posiEsperaCozinhero.Count);
        }
    }
    void Move()
    {
        anim.SetBool("isIdle", false);
        // Debug.Log("<color=yellow>Movendo</color>");
        agent.SetDestination(target.position);
    }


}
