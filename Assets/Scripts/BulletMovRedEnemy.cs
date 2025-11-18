using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovRedEnemy : MonoBehaviour
{
    [SerializeField] private float speed = 4f;

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
        if (transform.position.x <= -10f) 
        {
            Destroy(gameObject);
        }
    }
}
