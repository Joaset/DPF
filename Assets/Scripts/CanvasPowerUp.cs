using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasPowerUp : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private float duracion;
    void Start()
    {
        image = GetComponent<Image>();
        duracion = 7f;
    }

    public void EmpezarCorrutina()
    {
        StartCoroutine(VaciarBarra());
    }
    
    IEnumerator VaciarBarra()
    {
        image.fillAmount = 1f;
        float tiempo = 0f;

        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;
            image.fillAmount = 1f - (tiempo/duracion);
            yield return null;
        }
        image.fillAmount = 0f;
    }
}
