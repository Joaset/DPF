using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBoss : MonoBehaviour
{
    [SerializeField] private GameObject bulletBoss;
    [SerializeField] private Transform firePointBoss;
    void Start()
    {
        StartCoroutine("CreateBulletBoss");
    }

    void Update()
    {
        if (GetComponent<Boss>().GetVida() <= 0)
        {
            StopCoroutine("CreateBulletBoss");
        } 
    }

    IEnumerator CreateBulletBoss()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            Instantiate(bulletBoss, firePointBoss.position, Quaternion.identity);
        }
    }
}
