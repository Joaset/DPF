using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    void Start()
    {
        AudioManager.Instance.PlayAudio(AudioManager.Instance.winMusic);
    }

    public void CambiarMenu()
    {
        AudioManager.Instance.StopAudio(AudioManager.Instance.winMusic);
        GameManager.Instance.juegoIniciado = false;
        SceneManager.LoadScene(0);
    }
}
