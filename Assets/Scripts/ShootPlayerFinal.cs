using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPlayerFinal : MonoBehaviour
{
    [SerializeField] private Transform firePoint2;
    [SerializeField] private Transform firePoint3;
    [SerializeField] private GameObject bullet;
    private bool puedeDisparar;
    [SerializeField] private float tiempoEntreAtaques;
    [SerializeField] private float tiempoSiguienteAtaque;

    void Start()
    {
        puedeDisparar = true;
    }

    void Update()
    {
            Shoot();
    }

    void Shoot()
    {
        if (tiempoSiguienteAtaque > 0)
        {
            tiempoSiguienteAtaque -= Time.deltaTime;
        }
        if (Input.GetButtonDown("Jump") && tiempoSiguienteAtaque <= 0 && puedeDisparar == true)
        {
            Instantiate(bullet, firePoint2.position, firePoint2.rotation);
            Instantiate(bullet, firePoint3.position, firePoint3.rotation);
            AudioManager.Instance.PlayAudio(AudioManager.Instance.shoot);
            tiempoSiguienteAtaque = tiempoEntreAtaques;
        }
    }
}
