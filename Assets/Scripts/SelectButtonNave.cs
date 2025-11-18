using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectButtonNave : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject nave;
    [SerializeField] GameObject naveOscura;
    public Animator animationNave;

    void Start()
    {
        animationNave = nave.GetComponent<Animator>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        animationNave.SetBool("IsTouching",true);
        naveOscura.GetComponent<ColorNave>().CambiarColorNave();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animationNave.SetBool("IsTouching", false);
        naveOscura.GetComponent<ColorNave>().RegresarColorNave();
    }
}
