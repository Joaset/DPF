using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Meteo : MonoBehaviour
{
    [SerializeField] private Vector3 speed;

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            speed = new Vector3(2f, 0f, 0f); // velocidad en unidades/segundo
        }
        if (SceneManager.GetActiveScene().buildIndex == 6)
        {
            speed = new Vector3(0f, 2f, 0f); // velocidad en unidades/segundo
        }

    }
    void Update()
    {
        transform.position -= speed * Time.deltaTime;
        if (transform.position.y <= (-6))
        {
            Destroy(gameObject);
        }
        if (SceneManager.GetActiveScene().buildIndex == 4 && transform.position.x <= (-10))
        {
            Destroy(gameObject);
        }
    }
}
