using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    BulletScript Bullet;
    public AudioSource AS;
    public AudioClip Shooting, Upgrade;
    public Transform Targeting;
    public Transform TowerRotation;
    public string enemyTag = "Enemy";

    public Transform ShootingPoint;
    public GameObject BulletPrefab;
    public float Range = 10f;
    public float Firerate = 2f;
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

        Vector3 Direction = Targeting.position - transform.position;
        Quaternion Lookrotation = Quaternion.LookRotation(Direction);
        Vector3 rotation = Lookrotation.eulerAngles;
        TowerRotation.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (Cooldown <= 0f)
        {
            Shoot();
            Cooldown = 3f / Firerate;
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
        BulletScript bullet = BulletMovement.GetComponent<BulletScript>();
        bullet.Seek(Targeting);
    }
}
