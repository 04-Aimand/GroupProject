using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
	GameManager GameManager;
	HealthBar HealthBar;
	public float startHealth = 100;
	public float health;
	public int drop = 10;
	public Rigidbody RB;

	private bool isDead = false;

	void Start()
	{
		RB = GetComponent<Rigidbody>();
		health = startHealth;
	}

    void Update()
    {

    }

    public void TakeDamage(float amount)
	{
		health -= amount;
		HealthBar.UpdateHealth(amount);

		if (health <= 0)
		{
			Die();
		}
	}

	void Die()
	{
		GameManager.coins += drop;
		Destroy(gameObject);
	}
}
