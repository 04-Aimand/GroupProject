using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyWave : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public Text Wavetext;

    private int currentWave = 1;
    private int enemiesPerWave = 5;
    private int mininc = 2;
    private int maxinc = 4;

    private Coroutine spawnCoroutine;

    void Start()
    {
        spawnCoroutine = StartCoroutine(SpawnWaves());
    }

    void Update()
    {
        if (currentWave == 3)
        {
            StopCoroutine(spawnCoroutine);
        }
    }

    IEnumerator SpawnWaves()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            SpawnWave();
        }
    }

    void SpawnWave()
    {
        Wavetext.text = "Wave " + currentWave;

        for (int i = 0; i < enemiesPerWave; i++)
        {
            SpawnEnemy();
        }

        enemiesPerWave += Random.Range(mininc, maxinc + 1);

        currentWave++;
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}