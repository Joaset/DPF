using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpItem : MonoBehaviour
{
    [SerializeField] private float velocidad = 2f;
    private Rigidbody2D rb;
    private Camera cam;
    private float limiteX, limiteY;
    private float mitadAncho, mitadAlto;
    [SerializeField] private float margenHUD; // ajustalo según tu HUD

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;

        // Tamaño del sprite
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        mitadAncho = sr.bounds.extents.x;
        mitadAlto = sr.bounds.extents.y;

        // Velocidad inicial en diagonal
        rb.velocity = new Vector2(velocidad, velocidad);
    }

    void Update()
    {
        // Calcular límites de la cámara en este frame
        limiteY = cam.orthographicSize;
        limiteX = limiteY * cam.aspect;

        Vector3 pos = transform.position;

        // Rebote horizontal
        if (pos.x + mitadAncho >= limiteX)
        {
            pos.x = limiteX - mitadAncho; // corrige la posición
            rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
        }
        else if (pos.x - mitadAncho <= -limiteX)
        {
            pos.x = -limiteX + mitadAncho;
            rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
        }

        // Rebote vertical
        if (pos.y + mitadAlto >= limiteY)
        {
            pos.y = limiteY - mitadAlto;
            rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y);
        }
        else if (pos.y - mitadAlto <= (-limiteY + margenHUD))
        {
            pos.y = (-limiteY + margenHUD) + mitadAlto;
            rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y);
        }

        transform.position = pos; // aplicar la corrección
    }
}
