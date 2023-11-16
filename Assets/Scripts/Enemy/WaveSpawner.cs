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

    private int waveNumber;
    public int numberOfEnemyInTheseWave = 5;
    
    private void Update()
    {
        Wave.text = "Wave " + waveNumber.ToString() + "\n"+ GameManager.instance.currentEnemyInWave.ToString() +" Enemies";

        if(GameManager.instance.currentEnemyInWave <=0 && waveNumber < 11)
        {
            waveNumber++;
            GameManager.instance.currentEnemyInWave = numberOfEnemyInTheseWave;
            StartCoroutine(SpawnWave());
        }

        if (waveNumber >= 10)
        {
            GameManager.instance.GameWin();
        }
    }


    IEnumerator SpawnWave()
    {
        for(int i =0;i< numberOfEnemyInTheseWave; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }

        

        numberOfEnemyInTheseWave += 5;
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnpoint.position, spawnpoint.rotation);
    }

}
