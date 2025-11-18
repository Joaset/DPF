using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitInstruction : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) // Enter
        {
            SceneManager.LoadScene("Selection");
            GameManager.Instance.vidaMaxima = 100f;
        }
    }
}
