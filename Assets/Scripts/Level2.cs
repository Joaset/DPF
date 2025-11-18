using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2 : MonoBehaviour
{
    private bool transitionStarted = false;
    void Update()
    {
        if (!transitionStarted && GameManager.Instance.puntajeTotal >= 750)//750
        {
            transitionStarted = true;

            var pixelEffect = FindObjectOfType<PixelateEffect>();
            if (pixelEffect != null)
            {
                int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
                pixelEffect.PlayPixelateEffect(40f, 0.5f, nextIndex);
            }
            else
            {
                // Fallback: carga directa si el efecto no est� presente
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}
