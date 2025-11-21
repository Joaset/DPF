using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitInstruction : MonoBehaviour
{
    // Variables para detectar doble toque
    private float tiempoUltimoToque = 0f;
    private float tiempoMaximoEntreToques = 0.5f; // Tiempo máximo entre toques para considerar doble toque
    private Vector2 posicionUltimoToque = Vector2.zero;
    private float distanciaMaximaPermitida = 50f; // Distancia máxima en píxeles entre los dos toques
    private int contadorToques = 0;

    void Update()
    {
        // Tecla Enter en PC
        if (Input.GetKeyDown(KeyCode.Return))
        {
            CargarEscenaSeleccion();
        }

        // Detectar doble toque en móvil
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // Verificar si el tiempo desde el último toque es válido
                if (Time.time - tiempoUltimoToque < tiempoMaximoEntreToques)
                {
                    // Calcular la distancia entre el toque actual y el anterior
                    float distancia = Vector2.Distance(touch.position, posicionUltimoToque);

                    // Si la distancia es menor a la permitida, contar como segundo toque
                    if (distancia <= distanciaMaximaPermitida)
                    {
                        contadorToques++;

                        // Si ya tenemos 2 toques en la misma posición, es un doble toque válido
                        if (contadorToques >= 2)
                        {
                            CargarEscenaSeleccion();
                            contadorToques = 0; // Resetear contador
                        }
                    }
                    else
                    {
                        // Si está muy lejos, reiniciar el contador
                        contadorToques = 1;
                        posicionUltimoToque = touch.position;
                    }
                }
                else
                {
                    // Si pasó mucho tiempo, reiniciar el contador
                    contadorToques = 1;
                    posicionUltimoToque = touch.position;
                }

                tiempoUltimoToque = Time.time;
                
                // Actualizar la posición si es el primer toque
                if (contadorToques == 1)
                {
                    posicionUltimoToque = touch.position;
                }
            }
        }

        // Resetear el contador si pasa mucho tiempo sin tocar
        if (Time.time - tiempoUltimoToque > tiempoMaximoEntreToques)
        {
            contadorToques = 0;
        }
    }

    void CargarEscenaSeleccion()
    {
        SceneManager.LoadScene("Selection");
        GameManager.Instance.vidaMaxima = 100f;
    }
}