using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    public float probabilidadPowerUp = 0.03f; // para la probabilidad del power up
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            AudioManager.Instance.PlayAudio(AudioManager.Instance.enemydead);
        }


        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
            AudioManager.Instance.PlayAudio(AudioManager.Instance.enemydead);
            gameObject.GetComponent<Explosion>().CrearExplosion();
            if (GameManager.Instance.puntajeTotal == 100)
            {
                GetComponent<SpawnItem>().CrearItemVida();
            }
            if (Random.value < probabilidadPowerUp && FindAnyObjectByType<ShootPlayer>().GetItemCreado() == false)
            {
                // Si tiene componente SpawnPowerUp
                SpawnPowerUp spPower = GetComponent<SpawnPowerUp>();
                if (spPower != null) spPower.CrearItemPowerUp();
                FindAnyObjectByType<ShootPlayer>().SetItemCreado(true);
            }
        }
    }
}
