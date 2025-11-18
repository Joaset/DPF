using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreGameOver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        textMesh.text = "Puntaje: " + GameManager.Instance.puntajeTotal.ToString();
    }
}
