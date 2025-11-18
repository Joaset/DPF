using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioSource backgroundMusic, shoot, enemydead, bossDead, powerUp, winMusic, loseMusic, life, boss, backgroundBoss;
    public static AudioManager Instance;

    [Range(0f, 1f)] public float volumenGeneral = 1f; // volumen global (0 a 1)

    private void Awake()
    {
        if (AudioManager.Instance == null)
        {
            AudioManager.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // Aplica el volumen global a todos los audios activos
        backgroundMusic.volume = volumenGeneral;
        shoot.volume = volumenGeneral;
        enemydead.volume = volumenGeneral;
        bossDead.volume = volumenGeneral;
        powerUp.volume = volumenGeneral;
        winMusic.volume = volumenGeneral;
        loseMusic.volume = volumenGeneral;
        life.volume = volumenGeneral;
        boss.volume = volumenGeneral;
        backgroundBoss.volume = volumenGeneral;
    }

    public void PlayAudio(AudioSource audio)
    {
        audio.Play();
    }

    public void StopAudio(AudioSource audio)
    {
        audio.Stop();
    }

    // 🔹 Método para cambiar volumen desde el slider
    public void SetVolumenGeneral(float nuevoVolumen)
    {
        volumenGeneral = nuevoVolumen;
    }
}
