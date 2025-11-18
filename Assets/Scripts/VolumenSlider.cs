using UnityEngine;
using UnityEngine.UI;

public class VolumenSlider : MonoBehaviour
{
    [SerializeField] private Slider sliderVolumen;

    void Start()
    {
        // Inicializa el valor del slider al volumen actual
        if (sliderVolumen != null)
        {
            sliderVolumen.value = AudioManager.Instance.volumenGeneral;
            sliderVolumen.onValueChanged.AddListener(CambiarVolumen);
        }
    }

    private void CambiarVolumen(float valor)
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.SetVolumenGeneral(valor);
    }
}

