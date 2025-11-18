using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpCollider : MonoBehaviour
{
    public float duracion = 7f; // tiempo que dura el powerUp
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            ShootPlayer sp = collision.GetComponent<ShootPlayer>();
            if (sp != null)
            {
                sp.ActivarDisparoDoble(duracion);
            }
            Destroy(gameObject);
            AudioManager.Instance.PlayAudio(AudioManager.Instance.powerUp);
        }
    }
}
