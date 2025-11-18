using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NivelScore : MonoBehaviour
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
            textMesh.text = "Nivel - 1 ";
        }
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            textMesh.text = "Nivel - 2 ";
        }
        if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            textMesh.text = "Nivel - 3 ";
        }
        if (SceneManager.GetActiveScene().buildIndex == 6)
        {
            textMesh.text = "Nivel - Boss ";
        }
    }
}

