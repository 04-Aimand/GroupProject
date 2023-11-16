using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public LevelLoader levelLoader;

    public Text coinsText;
    public int coins;

    public float timeflow;

    public Text baseHealthText;
    private float baseHealth = 100;

    public int currentEnemyInWave;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = timeflow;
    }

    // Update is called once per frame
    void Update()
    {
        coinsText.text = coins + " Coins";
    }

    public void TakeDamage(int dmg)
    {
        baseHealth -= dmg;
        baseHealthText.text = "Health : " + baseHealth;

        if (baseHealth <= 0)
        {
            levelLoader.Lose();
        }
    }

    public void GainCoins(int amount)
    {
        coins += amount;
        coinsText.text = coins + " Coins";


    }

    public void BuyTower(int cost)
    {
        coins -= cost;
        coinsText.text = coins + " Coins";
    }

    public void GameWin()
    {
        levelLoader.Win();
    }

    public void EnemyDefeated()
    {
        currentEnemyInWave -= 1;
    }
    
}
