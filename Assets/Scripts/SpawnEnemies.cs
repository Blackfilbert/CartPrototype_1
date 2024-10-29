using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject[] waypoints;
    public GameObject enemyPrefab;
    public int maxEnemiesInWave = 10;
    public int timeBetweenWaves = 5;
    private float lastSpawnTime;
    private int enemiesSpawned = 0;
    public float spawnInterval = 2;

    private void Start() {
        lastSpawnTime = Time.time;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCartMovement>().waypoints = waypoints;
    }

    private void Update() {
        WaveSpawnLogic();
    }

    private void WaveSpawnLogic() {
            float timeInterval = Time.time - lastSpawnTime;
            if(((enemiesSpawned == 0 && timeInterval > timeBetweenWaves) || timeInterval > spawnInterval) && enemiesSpawned < maxEnemiesInWave) {
                
                lastSpawnTime = Time.time;
                GameObject newEnemy = (GameObject)
                    Instantiate(enemyPrefab);
                newEnemy.GetComponent<EnemyMovement>().waypoints = waypoints;
                enemiesSpawned++;
            }
            if(enemiesSpawned == maxEnemiesInWave && GameObject.FindGameObjectWithTag("Enemy") == null) {
                enemiesSpawned = 0;
                lastSpawnTime = Time.time;
            }
    }
}
