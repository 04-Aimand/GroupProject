﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Text coinsText;
    public int coins;

    public Text baseHealthText;
    private float baseHealth = 100;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
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

        if(baseHealth <= 0)
        {
            //Lose Game
        }
    }

    public void BuyTower(int cost)
    {
        coins -= cost;
        coinsText.text = coins + " Coins";
    }
}
