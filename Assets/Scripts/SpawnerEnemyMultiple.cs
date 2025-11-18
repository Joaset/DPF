using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemyMultiple : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float spawnInterval = 1f;

    void Start()
    {
        StartCoroutine(SpawnEnemigos());
    }

    private void CreateEnemy()
    {
        Camera cam = Camera.main;
        float limiteY = cam.orthographicSize;

       
        GameObject prefabSeleccionado = ElegirPrefab();

        
        GameObject enemy = Instantiate(prefabSeleccionado, Vector3.zero, Quaternion.identity);

        
        SpriteRenderer sr = enemy.GetComponent<SpriteRenderer>();
        float mitadAlto = sr.bounds.extents.y;

        
        float enemyOrigenVertical = Random.Range(-limiteY + mitadAlto, limiteY - mitadAlto);

        
        float spawnX = cam.orthographicSize * cam.aspect + 1f; // un poquito fuera a la derecha
        enemy.transform.position = new Vector3(spawnX, enemyOrigenVertical, 0f);
    }

    private GameObject ElegirPrefab()
    {
       
        float r = Random.value; 

        // 0.0 - 0.4 -> Enemy
        // 0.4 - 0.8 -> EnemyRed
        // 0.8 - 1.0 -> EnemyCuracion
        if (r < 0.4f)
            return enemyPrefabs[0]; 
        else if (r < 0.8f)
            return enemyPrefabs[1]; 
        else
            return enemyPrefabs[2]; 
    }

    private IEnumerator SpawnEnemigos()
    {
        while (true)
        {
            CreateEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
