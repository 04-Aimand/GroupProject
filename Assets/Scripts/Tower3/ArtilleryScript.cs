using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleryScript : MonoBehaviour
{
	EnemyScript EnemyScript;
	public Transform Target;
	public float Shootforce;
	public float Gravityvelocity;
	public float rotateSpeed;
	public float damage;
	public Rigidbody RB;
	public GameObject Impacteffect;
	public float explosionRadius;

	public void Seek(Transform _target)
	{
		Target = _target;
	}

	void Start()
	{
		StartCoroutine(Shoot());
	}

	IEnumerator Shoot()
	{
		yield return new WaitForSeconds(5.5f);
		RB.AddForce(transform.up * Shootforce, ForceMode.Impulse);
		yield return new WaitForSeconds(0.5f);
		InvokeRepeating("ConstantRotation", 0, 0.5f);
	}

    private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Enemy"))
		{
			HitTarget();
		}

		if (collision.gameObject.CompareTag("Terrain"))
		{
			HitTarget();
		}
	}

	void ConstantRotation()
    {
		if(Target == null)
        {
			return;
        }
		transform.LookAt(Target);
		transform.Translate(Vector3.forward * Shootforce * Time.deltaTime);
		Vector3 direction = Target.position;
		direction.Normalize();

		Vector3 amountToRotate = Vector3.Cross(direction, transform.forward) * Vector3.Angle(transform.forward, direction);

		RB.angularVelocity = -amountToRotate * rotateSpeed;

		RB.velocity = transform.forward * Shootforce;
	}

	void HitTarget()
	{
		GameObject effectIns = (GameObject)Instantiate(Impacteffect, transform.position, transform.rotation);
		Destroy(effectIns, 5f);

		if (explosionRadius > 0f)
		{
			Explode();
		}
		else
		{
			Damage(Target);
		}
		Destroy(gameObject);
	}

	void Explode()
	{
		Destroy(gameObject);
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
