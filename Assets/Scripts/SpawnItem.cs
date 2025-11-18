using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Transform itemPoint;
    [SerializeField] private bool itemVida;

    void Start()
    {
        itemVida = true;
    }

    public void CrearItemVida()
    {
        if (itemVida == true)
        {
            Instantiate(itemPrefab, itemPoint.position, Quaternion.identity);
            itemVida=false;
        }
    }
}
