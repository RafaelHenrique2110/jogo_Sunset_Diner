using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cliente : MonoBehaviour
{
    public static cliente instance;
    public enum Estados { ESPERA, IRPARACADEIRA, COMER, IREMBORA, SENTAR, INDOPARACADEIRA };
    public Estados current_state;
    public Estados novoEstado;
    public UnityEngine.AI.NavMeshAgent agent;
    public Transform target;
    Vector3 dir;
    public float speed, timeComer = 10.0f;
    public int qualcadeira = 0;
    public float tempoMovimento;
    public bool esperandoSerAtendido = false, fome = true, ativaTimeComer, estouNaCadeira = false, cadeiraEncontrada = false, procurarCadeira = false;
    public int tipoPedido;
    public bool fuiAtendido = false;
    public GameObject mao;
    public GameObject meuPedido;
    public GameObject minhaCadeira;
    public GameObject[] posiIrEmbora;
    public int index = 0;
    public bool estouMesa = false;
    public int tipoDistribuidora = 0;
    float currentSpeed;
    public ComidasOBS meuPedidoDistribuidora;
    public DistribuidoraOBS minhaDistribuidora;
    public Animator anim;
    public List<GameObject> particulas = new List<GameObject>();

    void Start()
    {
        instance = this;
        listas.instance.cliente.Add(this.gameObject);
        qualcadeira = Random.Range(0, listas.instance.cadeiras.Count);
        currentSpeed = agent.speed;
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        if (PauseManager.instance.gameState == PauseManager.GameStates.Resumed)
        {
            agent.speed = currentSpeed * PauseManager.instance.myTimeScale;
            VerificaCadeira();
            if (current_state != novoEstado)
            {
                current_state = novoEstado;

            }
            if (current_state == Estados.IRPARACADEIRA)
            {
                IrParaCadeira();
            }

            if (current_state == Estados.COMER)
            {
                Comer();
            }
            if (current_state == Estados.IREMBORA)
            {
                irEmbora();
                if (agent.remainingDistance < 0.1f && target != null)
                {

                    if (index == 1)
                    {
                        agent.enabled = false;
                        //  Destroy(gameObject);
                        gameObject.SetActive(false);

                    }
                    index = 1;
                }


            }
            if (current_state == Estados.SENTAR)
            {
                Sentar();
            }
            if (agent.enabled == true && agent.remainingDistance < 0.1f && target != null)
            {


                if (current_state == Estados.INDOPARACADEIRA)
                {
                    IndoParaCadeira();
                }

            }


            particulas[1].SetActive(false);

            if (listas.instance.CadeirasOcupadas.Count == 0)
            {
                particulas[1].SetActive(true);


            }
            if (fome == false)
            {
                novoEstado = Estados.IREMBORA;
            }
            if (timeComer <= 0)
            {

                novoEstado = Estados.COMER;

            }

            if (ativaTimeComer == true)
            {
                timeComer = timeComer - 1.0f * Time.deltaTime;

            }

        }
        else if (agent.speed > 0)
        {
            agent.speed = 0;
        }

    }
    void VerificaCadeira()
    {

        for (int i = 0; i < listas.instance.cadeiras.Count; i++)
        {
            if (listas.instance.cadeiras[i].GetComponent<cadeira>().vendida == true)
            {

                listas.instance.cadeiras.Remove(listas.instance.cadeiras[i]);
            }
        }
        if (listas.instance.cadeiras.Count == 0 && cadeiraEncontrada == false)
        {
            novoEstado = Estados.IREMBORA;

        }
    }
    void IrParaCadeira()
    {


        if (cadeiraEncontrada == false)
        {
            qualcadeira = Random.Range(0, listas.instance.cadeiras.Count);
            minhaCadeira = listas.instance.cadeiras[qualcadeira];
            cadeiraEncontrada = true;
        }
        target = minhaCadeira.transform;
        Move();
        if (target != null && DectorDistancia())
        {
            listas.instance.cadeiras.Remove(minhaCadeira);
            estouNaCadeira = true;
            Debug.Log("la");

            novoEstado = Estados.INDOPARACADEIRA;

        }

    }
    void IndoParaCadeira()
    {
        Debug.Log("uiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiicara");

        novoEstado = Estados.SENTAR;
    }
    void Sentar()
    {

        

        esperandoSerAtendido = true;

        agent.speed = 0;

        listas.instance.clienteEsperando.Add(this.gameObject);

        if (Inventario.Instance().distribuidorasContratadas.Count == 1)
        {
            tipoDistribuidora = 0;
            Debug.Log("---------------igual 1");
        }
        if (Inventario.Instance().distribuidorasContratadas.Count > 1)
        {
            tipoDistribuidora = Random.Range(0, Inventario.Instance().distribuidorasContratadas.Count);
            Debug.Log("---------------mauior 1");
        }
        if (Inventario.Instance().distribuidorasContratadas.Count >= 0)
        {
            minhaDistribuidora = Inventario.Instance().distribuidorasContratadas[tipoDistribuidora];
            if (minhaDistribuidora == null)
            {
                Debug.LogWarning("Não tem distribuidora");
                return;
            }
            int tipoComidasOBS = Random.Range(0, minhaDistribuidora.comidas.Count);
            meuPedidoDistribuidora = minhaDistribuidora.comidas[tipoComidasOBS];
            tipoPedido = meuPedidoDistribuidora.id;

            novoEstado = Estados.ESPERA;
        }
        else
        {
            novoEstado = Estados.IREMBORA;
        }
        anim.Play("Armature_WaitPose1");

    }
    void Comer()
    {

        listas.instance.cadeiras.Add(minhaCadeira);
        meuPedido.transform.parent = null;
        fome = false;
        meuPedido.GetComponent<alimento>().devorado = true;
        listas.instance.mesaSuja.Add(meuPedido);
        agent.speed = 2;
        ativaTimeComer = false;
        anim.SetBool("Eating", false);
        minhaCadeira.GetComponent<cadeira>().ocupado = false;
        particulas[0].SetActive(true);
        listas.instance.CadeirasOcupadas.Remove(minhaCadeira);
        meuPedido = null;
        minhaCadeira = null;
        timeComer = 10.0f;
        EstatisticaController.Instance().ClientesA();
        novoEstado = Estados.IREMBORA;
    }
    void irEmbora()
    {
        if (agent.enabled)
        {
            agent.speed = 2;

            target = posiIrEmbora[index].transform;

            Move();
        }

    }
    void Move()
    {
        anim.Play("Armature_WalkCycle");

        agent.SetDestination(target.position);


    }
    bool DectorDistancia()
    {
        return agent.remainingDistance < 1f;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("mesa") && esperandoSerAtendido == true)
        {
            other.gameObject.GetComponent<mesa>().clientesNaMesa.Add(this.gameObject);
            estouMesa = true;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("mesa"))
        {
            estouMesa = false;
        }
    }


}
