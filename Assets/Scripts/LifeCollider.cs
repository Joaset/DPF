using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            AudioManager.Instance.PlayAudio(AudioManager.Instance.life);
            collision.gameObject.GetComponent<Player>().AumentarVidaItem();
        }
    }
}
