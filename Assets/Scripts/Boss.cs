using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    [SerializeField] float lifeBoss;
    Animator animator;
    [SerializeField] private Color colorOriginal;
    [SerializeField] private SpriteRenderer spriteRenderer;

    void Start()
    {
        AudioManager.Instance.PlayAudio(AudioManager.Instance.boss);
        lifeBoss = 2000f;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        colorOriginal = spriteRenderer.color;
    }

    public void RestarVida()
    {
        lifeBoss -= 10f;
        if (lifeBoss >= 10)
        {
            StartCoroutine(CambiarColor(Color.red));
        }
        else if(lifeBoss == 0)
        {
            GetComponent<CapsuleCollider2D>().enabled = false;
            StartCoroutine("CambiarEscena");
            AudioManager.Instance.StopAudio(AudioManager.Instance.boss);
            AudioManager.Instance.PlayAudio(AudioManager.Instance.bossDead);
            animator.SetBool("IsDead",true);
        }
    }

    IEnumerator CambiarEscena()
    {
        yield return new WaitForSeconds(4f);
        AudioManager.Instance.StopAudio(AudioManager.Instance.backgroundBoss);
        SceneManager.LoadScene("Win");
    }

    IEnumerator CambiarColor(Color nuevoColor)
    {
        spriteRenderer.color = nuevoColor;
        yield return new WaitForSeconds(0.2f); // dura 0.2 segundos
        spriteRenderer.color = colorOriginal; // vuelve al color original
    }

    public float GetVida()
    {
        return lifeBoss;
    }
}
