using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBulletEnemy : MonoBehaviour
{
   [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bullet;

    [SerializeField] private float tiempoEntreAtaques = 1f; // tiempo entre cada bala

    private void Start()
    {
        // Inicia la corutina que dispara balas automáticamente
        StartCoroutine(DispararContinuo());
    }

    private void Disparar()
    {
        Instantiate(bullet, firePoint.position, firePoint.rotation);
    }

    private IEnumerator DispararContinuo()
    {
        while (true)
        {
            Disparar();
            yield return new WaitForSeconds(tiempoEntreAtaques);
        }
    }
}
