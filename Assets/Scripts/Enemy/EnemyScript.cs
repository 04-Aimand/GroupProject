using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
	public float startSpeed = 10f;

	[HideInInspector]
	public float speed;

	public float startHealth = 100;
	private float health;

	public int worth = 10;

	public GameObject deathEffect;
	HealthBar HealthBar;

	public Slider healthBar;

	private bool isDead = false;
	void Start()
	{
		speed = startSpeed;
		health = startHealth;
	}

	public void TakeDamage(float amount)
	{
		health -= amount;

		healthBar.value = health / startHealth;

		if (health <= 0 && !isDead)
		{
			Die();
		}
	}

	public void Slow(float pct)
	{
		speed = startSpeed * (1f - pct);
	}

	void Die()
	{
		GameManager.instance.EnemyDefeated();
		isDead = true;
		Instantiate(deathEffect, transform.position, Quaternion.identity);
		GameManager.instance.GainCoins(worth);
		Destroy(gameObject);
	}
}

