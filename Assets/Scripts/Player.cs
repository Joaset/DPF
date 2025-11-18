using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private Color colorOriginal;
    [SerializeField] private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        colorOriginal = spriteRenderer.color;
    }
    void Update()
    {
        if (GameManager.Instance.vidaMaxima <= 0)
        {
            SceneManager.LoadScene("GameOver");
            AudioManager.Instance.StopAudio(AudioManager.Instance.backgroundMusic);
            AudioManager.Instance.StopAudio(AudioManager.Instance.boss);
            AudioManager.Instance.StopAudio(AudioManager.Instance.backgroundBoss);
            Destroy(gameObject);
        }
    }

    public void RestarVida()
    {
        GameManager.Instance.vidaMaxima -= 25;
        StartCoroutine(CambiarColor(Color.red));
    }

    public void AumentarVida()
    {
        if (GameManager.Instance.vidaMaxima < 200)
        {
             GameManager.Instance.vidaMaxima += 25;
            StartCoroutine(CambiarColor(Color.green));
            AudioManager.Instance.PlayAudio(AudioManager.Instance.life);
        }
        else
        {
            GameManager.Instance.vidaMaxima = 200;
        }
        /*if (GameManager.Instance.vidaMaxima <=75 && GameManager.Instance.vidaMaxima > 0)
        {
            GameManager.Instance.vidaMaxima += 25;
            StartCoroutine(CambiarColor(Color.green));
            AudioManager.Instance.PlayAudio(AudioManager.Instance.life);
            
        }
        else if (GameManager.Instance.vidaMaxima > 75 && GameManager.Instance.vidaMaxima <= 100)
        {
            GameManager.Instance.vidaMaxima = 100;
        }*/
    }

    public void AumentarVidaItem()
    {
        GameManager.Instance.vidaMaxima += 100f;
        StartCoroutine(CambiarColor(Color.green));
    }

    IEnumerator CambiarColor(Color nuevoColor)
    {
        spriteRenderer.color = nuevoColor;
        yield return new WaitForSeconds(0.2f); // dura 0.2 segundos
        spriteRenderer.color = colorOriginal; // vuelve al color original
    }
}
