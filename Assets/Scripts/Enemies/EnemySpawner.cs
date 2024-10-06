using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemies;
    public Transform[] spawnPoints;

    public float minSpawnTime = 3f; 
    public float maxSpawnTime = 6f;

    public AudioClip spawnSound;
    public AudioSource audioSource;

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            // Elegir el tiempo aleatorio entre 3 y 6 segundos
            float spawnInterval = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(spawnInterval);

            // Elegir aleatoriamente un punto de spawn
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Elegir el enemigo basado en las probabilidades
            GameObject enemyToSpawn = ChooseEnemy();

            // Instanciar el enemigo en el punto de spawn
            Instantiate(enemyToSpawn, spawnPoint.position, spawnPoint.rotation);
            audioSource.pitch = Random.Range(1.0f, 2.0f);
            audioSource.PlayOneShot(spawnSound);
        }
    }

    GameObject ChooseEnemy()
    {
        // Generar un número entre 0 y 100 para la probabilidad de cada enemigo
        //Chaser 50%, shooter 30% y dragon 20%
        float randomValue = Random.Range(0f, 100f);

        if (randomValue <= 50f)
        {
            return enemies[0];
        }
        else if (randomValue <= 80f)
        {
            return enemies[1];
        }
        else
        {
            return enemies[2];
        }
    }
}
