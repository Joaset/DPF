using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnerEnemy : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab; // Asignar el prefab en el Inspector
    [SerializeField] private int tipoEnemigo;
    [SerializeField] private float margenHUDArriba;
    [SerializeField] private float margenHUDAbajo;

    void Start()
    {
        StartCoroutine(SpawnEnemigos());
    }
    public void CreateEnemy()
    {
        Camera cam = Camera.main;
        float limiteY = cam.orthographicSize;

        // Instanciamos primero para poder calcular el tama�o real del sprite
        GameObject enemy = Instantiate(enemyPrefab, Vector3.zero, enemyPrefab.transform.rotation);

        // Obtenemos la mitad del alto del sprite
        SpriteRenderer sr = enemy.GetComponent<SpriteRenderer>();
        float mitadAlto = sr.bounds.extents.y;

        // Calculamos una posici�n Y v�lida
        float enemyOrigenVertical = Random.Range(-limiteY + mitadAlto + margenHUDAbajo, limiteY - mitadAlto - margenHUDArriba);

        // Colocamos el enemigo en X fijo (fuera de la pantalla) y en Y calculado
        float spawnX = cam.orthographicSize * cam.aspect + 1f; // un poquito fuera a la derecha
        enemy.transform.position = new Vector3(spawnX, enemyOrigenVertical, 0f);
    }

    IEnumerator SpawnEnemigos()
    {
        while (true) // bucle infinito como setInterval
        {
            if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                CreateEnemy();
                yield return new WaitForSeconds(1f);
            }
            if (SceneManager.GetActiveScene().buildIndex == 4)
            {
                CreateEnemy();
                yield return new WaitForSeconds(0.5f);
            }
            if (SceneManager.GetActiveScene().buildIndex == 5)
            {
                if (tipoEnemigo == 1)
                {
                    CreateEnemy();
                    yield return new WaitForSeconds(0.3f);
                }
                if (tipoEnemigo == 2)
                {
                    CreateEnemy();
                    yield return new WaitForSeconds(1.2f);
                }
                if (tipoEnemigo == 3)
                {
                    CreateEnemy();
                    yield return new WaitForSeconds(5f);
                }
            }
        }
    }
}
