using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class HoverText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text texto;

    public void OnPointerEnter(PointerEventData eventData)
    {
        texto.color = Color.black; // Al entrar el mouse
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        texto.color = Color.cyan; // Al salir el mouse
    }
}
