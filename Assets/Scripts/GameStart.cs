using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public void StartGame()
    {
        var pixelEffect = FindObjectOfType<PixelateEffect>();
        if (pixelEffect != null)
        {
            AudioManager.Instance.StopAudio(AudioManager.Instance.backgroundMusic);
            int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
            pixelEffect.PlayPixelateEffect(40f, 0.5f, nextIndex);
        }
        else
        {
            // Fallback: carga directa si el efecto no está presente
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            AudioManager.Instance.StopAudio(AudioManager.Instance.backgroundMusic);
        }
    }

    public void SeleccionarJugador(float nave)
    {
        GameManager.Instance.jugador = nave;    
    }
}
