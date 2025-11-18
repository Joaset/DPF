using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    void Start()
    {
        AudioManager.Instance.PlayAudio(AudioManager.Instance.loseMusic);
    }

    public void CambiarMenu()
    {
        AudioManager.Instance.StopAudio(AudioManager.Instance.loseMusic);
        GameManager.Instance.juegoIniciado = false;
        SceneManager.LoadScene(0);
    }
}
