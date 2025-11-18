using UnityEngine;
using UnityEngine.UI;

public class AlphaController : MonoBehaviour
{
    [SerializeField] private Image imagen;          // Referencia al objeto Image
    [SerializeField] private float alphaInicial = 45f;
    [SerializeField] private float alphaFinal = 200f;
    [SerializeField] private float escalaMin = 1.9f;
    [SerializeField] private float escalaMax = 2.1f;
    [SerializeField] private float duracion = 2f;   // Tiempo del ciclo completo

    private float tiempo = 0f;
    private bool aumentando = true;

    void Start()
    {
        if (imagen == null)
            imagen = GetComponent<Image>();

        // Configura alpha inicial
        Color c = imagen.color;
        c.a = alphaInicial / 255f;
        imagen.color = c;

        // Escala inicial
        transform.localScale = Vector3.one * escalaMin;
    }

    void Update()
    {
        if (imagen == null) return;

        // Progresión normalizada (0 → 1)
        tiempo += Time.deltaTime / duracion;

        // Interpolación de alpha y escala
        float alphaActual, escalaActual;

        if (aumentando)
        {
            alphaActual = Mathf.Lerp(alphaInicial, alphaFinal, tiempo);
            escalaActual = Mathf.Lerp(escalaMin, escalaMax, tiempo);
        }
        else
        {
            alphaActual = Mathf.Lerp(alphaFinal, alphaInicial, tiempo);
            escalaActual = Mathf.Lerp(escalaMax, escalaMin, tiempo);
        }

        // Aplicar alpha
        Color c = imagen.color;
        c.a = alphaActual / 255f;
        imagen.color = c;

        // Aplicar escala
        transform.localScale = Vector3.one * escalaActual;

        // Reiniciar ciclo
        if (tiempo >= 1f)
        {
            tiempo = 0f;
            aumentando = !aumentando;
        }
    }
}

