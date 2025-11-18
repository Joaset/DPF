using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        if (GameManager.Instance.juegoIniciado == false)
        {
            AudioManager.Instance.PlayAudio(AudioManager.Instance.backgroundMusic);
            GameManager.Instance.juegoIniciado = true;
        }
        //AudioManager.Instance.PlayAudio(AudioManager.Instance.backgroundMusic);
        GameManager.Instance.puntajeTotal = 0;
        GameManager.Instance.vidaMaxima = 100;
        GameManager.Instance.jugador = 0;
    }

    public void Jugar()
    {
        var pixelEffect = FindObjectOfType<PixelateEffect>();
        if (pixelEffect != null)
        {
            int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
            pixelEffect.PlayPixelateEffect(40f, 0.5f, nextIndex);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    // Ir a la escena de opciones
    public void IrAOpciones()
    {
        SceneManager.LoadScene("Opciones");
    }

    // 🔹 Volver al menú principal
    public void VolverMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    // 🔹 Salir del juego
    public void SalirDelJuego()
    {
        Debug.Log("Saliendo del juego...");

#if UNITY_EDITOR
        // Si estás en el editor, detiene el modo Play
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Si estás en una build, cierra la aplicación
        Application.Quit();
#endif
    }
}

