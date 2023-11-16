using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform spawnpoint;
    public Text Wave;

    public float timeBetweenWave = 10f;
    private float countdown = 2f;
    private int waveNumber = 1;

    private void Update()
    {
        if(countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWave;
        }
        countdown -= Time.deltaTime;
        Wave.text = waveNumber.ToString()+" : Wave";
    }

    IEnumerator SpawnWave()
    {
        for(int i =0;i<waveNumber;i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
        waveNumber++;

        if(waveNumber >= 10)
        {
            GameManager.instance.GameWin();
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnpoint.position, spawnpoint.rotation);
    }
}
