using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    private static ParticleController instance;
    //public GameObject happyParticle;
    //public GameObject angryParticle;
    //public GameObject salaryParticle;
    //public GameObject moneyParticle;
    public GameObject rotateParticle;
    public GameObject sellParticle;

    private void Awake()
    {
        instance = this;
    }

    public static ParticleController Instance()
    {
        return instance;
    }
}
