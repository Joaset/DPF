using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorNave : MonoBehaviour
{
    [SerializeField] private Image nave;
    [SerializeField] private Color colorOriginal;
    void Start()
    {
        nave = GetComponent<Image>();
        colorOriginal = nave.color;
    }

    public void CambiarColorNave()
    {
        nave.color = Color.white;
    }

    public void RegresarColorNave()
    {
        nave.color = colorOriginal;
    }
}
