using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovNaveGreen : MonoBehaviour
{

    [SerializeField] private float speed = 2.5f;
    [SerializeField] private float amplitude = 0.5f;
    [SerializeField] private float frequency = 2f;

    private float startY;
    private float randomPhase;

    
    private float minY;
    private float maxY;

    private float localTime = 0f;
    private int direction = 1; // 1 = normal, -1 = invertido

    void Start()
    {
        startY = transform.position.y;
        randomPhase = Random.Range(0f, 2f * Mathf.PI); // fase aleatoria para variar el movimiento

        
        Camera cam = Camera.main;
        float camDistance = Mathf.Abs(cam.transform.position.z - transform.position.z);
        Vector3 bottom = cam.ViewportToWorldPoint(new Vector3(0, 0, camDistance));
        Vector3 top = cam.ViewportToWorldPoint(new Vector3(0, 1, camDistance));

        
        float margen = 0.5f;
        minY = bottom.y + margen;
        maxY = top.y - margen;

        
    }

    void Update()
    {
        float newX = transform.position.x - speed * Time.deltaTime;

    
        localTime += Time.deltaTime * direction * frequency;

        float newY = startY + Mathf.Sin(localTime) * amplitude;

    
    if (newY >= maxY || newY <= minY)
        {
            direction *= -1; // invierte sentido del movimiento
        }

        transform.position = new Vector3(newX, newY, transform.position.z);

    if (transform.position.x <= -10f)
        {
            Destroy(gameObject);
        }
    }
    
}
