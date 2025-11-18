using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFinal : MonoBehaviour
{
    [SerializeField] private Vector3 speed;

    void Start()
    {
        speed = new Vector3(0f, 10f, 0f); // velocidad en unidades/segundo
    }
    void Update()
    {
        transform.position += speed * Time.deltaTime;
        if (transform.position.y >= (10.92))
        {
            Destroy(gameObject);
        }
    }
}
