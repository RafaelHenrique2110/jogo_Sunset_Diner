using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onibus : MonoBehaviour
{
    public enum Estados { IRPARABAR, ESPERAR, SAIR };
    public Estados current_state;
    public Estados novoEstado;
    public Transform[] pontos;
    public int index = 0;
    public float velocidade;
    public GameObject clientes;
    public GameObject corpo;
    public int limiteSpawner;
    public float limiteCronometro;
    public int contador;
    public float timeCount;
    public bool ativarCronometro = false;


    private void Start()
    {
        timeCount = 0;

    }

    private void FixedUpdate()
    {

        if (current_state != novoEstado)
        {
            current_state = novoEstado;

        }

        if (PauseManager.instance.gameState == PauseManager.GameStates.Resumed)
        {
            switch (current_state)
            {
                case Estados.IRPARABAR: Move(); break;
                case Estados.ESPERAR: Espera(); break;
                case Estados.SAIR: Sair(); break;

            }


            if (index == 2)
            {
                novoEstado = Estados.ESPERAR;

            }
            if (index == 0)
            {
                corpo.SetActive(false);

            }
            if (index == 1)
            {
                corpo.SetActive(true);

            }


            if (ativarCronometro)
            {
                cronometro();

            }

        }
        else
        {
            return;
        }


    }

    void cronometro()
    {
        timeCount = timeCount + 1 * Time.deltaTime;
    }


    void Espera()
    {
        Spawner();
    }

    void Sair()
    {
        Move();
        //volta para inicio
    }

    void Move()
    {
        Vector3 direcao = pontos[index].position - transform.position;
        transform.rotation = Quaternion.LookRotation(direcao);
        transform.position += direcao.normalized * velocidade * Time.fixedDeltaTime;
        if ((direcao.magnitude <= 0.1f))
        {
            index = (++index) % pontos.Length;
        }
    }

    void Spawner()
    {
        ativarCronometro = true;

        if (timeCount >= limiteCronometro)
        {
            contador++;
            Instantiate(clientes, transform.position, transform.rotation);
            timeCount = 0;
        }
        if (contador >= limiteSpawner)
        {
            limiteSpawner = +5;
            ativarCronometro = false;
            index = 3;
            timeCount = 0;
            contador = 0;
            novoEstado = Estados.SAIR;

        }




    }
}

