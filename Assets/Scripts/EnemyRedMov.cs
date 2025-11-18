using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRedMov : MonoBehaviour
{
    //[SerializeField] private Vector3 speed = new Vector3(2f, 0f, 0f);
    // valor por defecto, pero se puede cambiar en Inspector
    [SerializeField] private float speed = 2.5f;
    void Update()
    {
        //transform.position -= speed * Time.deltaTime;
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x <= -10f)
        {
            Destroy(gameObject);
        }
    }
}

