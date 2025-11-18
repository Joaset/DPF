using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    [SerializeField] private Transform naveEnemiga; 
    
    public void CrearExplosion()
    {
        GameObject efectoExplosion = Instantiate(explosion,naveEnemiga.position,naveEnemiga.rotation);
        Destroy(efectoExplosion, 1f);
    }
}
