using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBoss : MonoBehaviour
{
    [SerializeField] private float velocidad = 0.7f;   // qué tan rápido se mueve
    [SerializeField] private float ancho = 6f;       // ancho del 8
    [SerializeField] private float alto = 1f;        // alto del 8
    private Vector2 direccion;

    private float tiempo;
    private Vector3 posicionInicial;

    void Start()
    {
        // Guardamos la posición inicial del objeto
        posicionInicial = transform.position;
    }

    void Update()
    {
        MoverDiagonal();
        if (GetComponent<Boss>().GetVida() > 0)
        {
            tiempo += Time.deltaTime * velocidad;

            // Desplazamiento en forma de 8
            float x = Mathf.Sin(tiempo) * ancho;
            float y = Mathf.Sin(tiempo * 2) * alto / 2f;

            // Sumamos el offset a la posición inicial
            transform.position = posicionInicial + new Vector3(x, y, 0f);
        }
        else
        {
            velocidad = 1f;
            transform.Translate(direccion * velocidad * Time.deltaTime);
        }
    }

    public void MoverDiagonal()
    {
        // Decidir dirección según posición actual
        if (transform.position.x >= 0)
            direccion = new Vector2(1f, 1f);  // derecha-arriba
        else
            direccion = new Vector2(-1f, 1f); // izquierda-arriba

        direccion = direccion.normalized;
    }
}
