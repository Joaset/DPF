

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LifeCount : MonoBehaviour
{
    [Header("Referencias UI")]
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private Slider barraVida;     // slider principal (0-100)
    [SerializeField] private Slider barraVidaExtra; // slider de vida extra (verde)

    [Header("Ajustes")]
    [SerializeField] private float maxVidaBase = 100f;
    [SerializeField] private float velocidadLerp = 5f;

    private float vidaActual;

    void Start()
    {
        if (textMesh == null)
            textMesh = GetComponent<TextMeshProUGUI>();

        if (barraVida == null || barraVidaExtra == null)
            Debug.LogWarning("Asigna ambos sliders (barraVida y barraVidaExtra) en el inspector.");

        // Configurar sliders
        barraVida.minValue = 0;
        barraVida.maxValue = maxVidaBase;
        barraVida.value = maxVidaBase;

        barraVidaExtra.minValue = 0;
        barraVidaExtra.maxValue = maxVidaBase;
        barraVidaExtra.value = 0;
        barraVidaExtra.gameObject.SetActive(false); // inicialmente desactivado
    }

    void Update()
    {
        vidaActual = GameManager.Instance.vidaMaxima;

        // Actualiza texto (opcional)
        textMesh.text = "Vida: ";// + vidaActual.ToString("0");

        ActualizarBarras();
    }

    private void ActualizarBarras()
    {
        // Caso 1: vida extra (más de 100)
        if (vidaActual > maxVidaBase)
        {
            barraVidaExtra.gameObject.SetActive(true);

            float exceso = vidaActual - maxVidaBase;
            exceso = Mathf.Clamp(exceso, 0, maxVidaBase);

            // la barra base se llena completa
            barraVida.value = Mathf.Lerp(barraVida.value, maxVidaBase, Time.deltaTime * velocidadLerp);
            // la barra extra muestra solo el exceso
            barraVidaExtra.value = Mathf.Lerp(barraVidaExtra.value, exceso, Time.deltaTime * velocidadLerp);
        }
        // Caso 2: vida normal (100 o menos)
        else
        {
            // esconder la barra extra si ya no hay exceso
            barraVidaExtra.value = Mathf.Lerp(barraVidaExtra.value, 0, Time.deltaTime * velocidadLerp);

            if (barraVidaExtra.value <= 1f)
                barraVidaExtra.gameObject.SetActive(false);

            // actualizar barra normal
            barraVida.value = Mathf.Lerp(barraVida.value, vidaActual, Time.deltaTime * velocidadLerp);
        }
    }
}




/*using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LifeCount : MonoBehaviour
{
    private TextMeshProUGUI textMesh;

    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        textMesh.text = "Vida: " + GameManager.Instance.vidaMaxima.ToString() + "%";
    }
}*/
