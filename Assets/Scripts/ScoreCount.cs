using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreCount : MonoBehaviour
{
    private TextMeshProUGUI textMesh;

    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            textMesh.text = "Puntaje: " + GameManager.Instance.puntajeTotal.ToString() + "/250";
        }
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            textMesh.text = "Puntaje: " + GameManager.Instance.puntajeTotal.ToString() + "/750";
        }
        if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            textMesh.text = "Puntaje: " + GameManager.Instance.puntajeTotal.ToString() + "/2000";
        }
        if (SceneManager.GetActiveScene().buildIndex == 6)
        {
            textMesh.text = "Puntaje: " + GameManager.Instance.puntajeTotal.ToString();
        }
    }
}
