using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollider : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameManager.Instance.puntajeTotal += 10f;
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("EnemyRojo"))
        {
            GameManager.Instance.puntajeTotal += 20f;
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("EnemyCuracion"))
        {
            FindAnyObjectByType<Player>().AumentarVida();
            Destroy(gameObject);
        }


        if (collision.gameObject.CompareTag("Meteo"))
        {
            Destroy(gameObject);
        }
    }
}
