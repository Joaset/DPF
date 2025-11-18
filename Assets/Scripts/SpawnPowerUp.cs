using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPowerUp : MonoBehaviour
{
    [SerializeField] private GameObject PowerUpPrefab;
    [SerializeField] private Transform PowerUpPoint;
    [SerializeField] private bool itemPowerUp;

    void Start()
    {
        itemPowerUp = true;
    }

    public void CrearItemPowerUp()
    {
        if (itemPowerUp == true)
        {
            Instantiate(PowerUpPrefab, PowerUpPoint.position, Quaternion.identity);
            itemPowerUp = false;
        }
    }
}
