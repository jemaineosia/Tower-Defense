using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveDetails
{
    public int basicEnemy;
    public int fastEnemy;

}

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private WaveDetails currentWave;
    [SerializeField] private Transform respawn;
    [SerializeField] private float spawnCooldown = 5f;
    private float spawnTimer;

    [SerializeField] private List<GameObject> enemiesToCreate;
    
    [Header("Enemies Prefabs")]
    [SerializeField] private GameObject basicEnemy;
    [SerializeField] private GameObject fastEnemy;

    private void Start()
    {
        enemiesToCreate = NewEnemyWave();
    }

    private void Update()
    {
        // Decrease the timer
        spawnTimer -= Time.deltaTime;

        // Check if it's time to spawn
        if (spawnTimer <= 0f)
        {
            CreateEnemy();
            // Reset the timer
            spawnTimer = spawnCooldown;
        }
    }

    private void CreateEnemy()
    {
        // Check if we need to generate a new wave
        if (enemiesToCreate.Count == 0)
        {
            enemiesToCreate = NewEnemyWave();
            return;
        }

        // Select random enemy from the list
        int randomIndex = Random.Range(0, enemiesToCreate.Count);
        GameObject enemyToSpawn = enemiesToCreate[randomIndex];

        // Create the enemy
        var newEnemy = Instantiate(enemyToSpawn, respawn.position, Quaternion.identity);

        // Remove the spawned enemy from the list to avoid repetition
        enemiesToCreate.RemoveAt(randomIndex);
    }

    private List<GameObject> NewEnemyWave()
    {
         List<GameObject> newEnemyList = new List<GameObject>();
        for (int i = 0; i < currentWave.basicEnemy; i++)
        {
            newEnemyList.Add(basicEnemy);
        }
        for (int i = 0; i < currentWave.fastEnemy; i++)
        {
            newEnemyList.Add(fastEnemy);
        }
        return newEnemyList;
    }
}