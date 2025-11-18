using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollider : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GetComponent<Player>().RestarVida();
        }
        if (collision.gameObject.CompareTag("EnemyRojo"))
        {
            GetComponent<Player>().RestarVida();
        }
        if (collision.gameObject.CompareTag("EnemyCuracion"))
        {
            GetComponent<Player>().AumentarVida();
        }
    }
}
