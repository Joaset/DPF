using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnimTextIns : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI texto;
    [SerializeField] private float velocidadFade = 2f;       // velocidad de aparición/desaparición
    [SerializeField] private float tiempoVisible = 2f;       // cuánto tiempo permanece visible
    [SerializeField] private float tiempoInvisible = 1f;     // cuánto tiempo permanece invisible
    [SerializeField] private float velocidadEscala = 2f;     // velocidad del agrandamiento
    [SerializeField] private float amplitudEscala = 0.2f;    // cuánto se agranda/achica

    private bool visible = false;
    private Vector3 escalaInicial;

    void Start()
    {
        if (texto == null)
            texto = GetComponent<TextMeshProUGUI>();

        escalaInicial = transform.localScale;
        StartCoroutine(CicloFade());
    }

    void Update()
    {
        if (texto == null) return;

        // ---- FADE ----
        Color color = texto.color;
        float alphaObjetivo = visible ? 1f : 0f;
        color.a = Mathf.MoveTowards(color.a, alphaObjetivo, Time.deltaTime * velocidadFade);
        texto.color = color;

        // ---- ESCALA ----
        float factor = 1f + Mathf.Sin(Time.time * velocidadEscala) * amplitudEscala;
        transform.localScale = escalaInicial * factor;
    }

    System.Collections.IEnumerator CicloFade()
    {
        while (true)
        {
            // Aparece
            visible = true;
            yield return new WaitForSeconds(tiempoVisible);

            // Desaparece
            visible = false;
            yield return new WaitForSeconds(tiempoInvisible);
        }
    }
}
