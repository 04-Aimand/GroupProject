using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleryShootingScript : MonoBehaviour
{
    ArtilleryScript Bullet;
    public AudioSource AS;
    public AudioClip Shooting;
    public Transform Targeting;
    public string enemyTag = "Enemy";

    public Transform ShootingPoint;
    public GameObject BulletPrefab;
    public float Range = 10f;
    private float Cooldown = 0f;

    void Start()
    {
        //AS.clip = StartingUp;
        //AS.Play();
        InvokeRepeating("DetectEnemiesInRadius", 0f, 0.5f);
    }


    void Update()
    {
        if (Targeting == null)
            return;

        if (Cooldown <= 0f)
        {
            Shoot();
            Cooldown = 6f;
        }
        Cooldown -= Time.deltaTime;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, Range);
    }


    void DetectEnemiesInRadius()
    {
        
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float closestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && closestDistance <= Range)
        {
            Targeting = nearestEnemy.transform;
        }
        else
        {
            Targeting = null;
        }
    }
    void Shoot()
    {
        GameObject BulletMovement = (GameObject)Instantiate(BulletPrefab, ShootingPoint.transform.position, ShootingPoint.transform.rotation);
        ArtilleryScript bullet = BulletMovement.GetComponent<ArtilleryScript>();
        bullet.Seek(Targeting);
    }
}
