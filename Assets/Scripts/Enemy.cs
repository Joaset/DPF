using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Vector3 speed;

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
          speed = new Vector3(2f, 0f, 0f); // velocidad en unidades/segundo  
        }
        if (SceneManager.GetActiveScene().buildIndex == 4 || SceneManager.GetActiveScene().buildIndex == 5)
        {
            speed = new Vector3(3f, 0f, 0f); // velocidad en unidades/segundo  
        }
        

    }
    void Update()
    {
        transform.position -= speed * Time.deltaTime;
        if (transform.position.x <= (-10))
        {
            Destroy(gameObject);
        }
    }
}
