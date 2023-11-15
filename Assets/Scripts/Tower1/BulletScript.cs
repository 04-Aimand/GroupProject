using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
	EnemyScript EnemyScript;
	private Transform target;
	public float bulletspeed = 2f;
	public float damage = 20f;
	public Rigidbody RB;
	public GameObject impactEffect;
	public float explosionRadius = 0f;

	public void Seek(Transform _target)
	{
		target = _target;
	}

    void Start()
    {
		transform.LookAt(target);
	}

    void Update()
	{
		StartCoroutine(Shoot());
	}

	IEnumerator Shoot()
    {
        yield return new WaitForSeconds(0.2f);
		transform.Translate(Vector3.forward * bulletspeed * Time.deltaTime);
	}

    private void OnCollisionEnter(Collision hit)
    {
		if (hit.gameObject.CompareTag("Enemy"))
		{
			HitTarget();
		}
    }

    void HitTarget()
	{
		GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
		Destroy(effectIns, 5f);
		Explode();
		Damage(target);
		Destroy(gameObject);
	}

	void Explode()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
		foreach (Collider collider in colliders)
		{
			if (collider.tag == "Enemy")
			{
				Damage(collider.transform);
			}
		}
	}

	void Damage(Transform enemy)
	{
		EnemyScript e = enemy.GetComponent<EnemyScript>();

		if (e != null)
		{
			e.TakeDamage(damage);
		}
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, explosionRadius);
	}
}
