using UnityEngine;
using UnityEngine.UI;

public class BrilloSlider : MonoBehaviour
{
    [SerializeField] private Slider sliderBrillo;

    void Start()
    {
        if (sliderBrillo != null)
        {
            sliderBrillo.value = BrilloManager.Instance.brilloActual;
            sliderBrillo.onValueChanged.AddListener(CambiarBrillo);
        }
    }

    private void CambiarBrillo(float valor)
    {
        BrilloManager.Instance.CambiarBrillo(valor);
    }
}
