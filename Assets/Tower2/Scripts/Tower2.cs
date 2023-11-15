using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower2 : MonoBehaviour
{
    private Transform target;
    //public Enemy targetEnemy;

    [Header("Attributes")]

    public float range = 15f;

    [Header("Use Bullets (Default)")]
    public GameObject bulletPrefab;
    public float firerate = 1f;
    private float firecountdown = 0f;

    [Header("Use Laser")]
    public bool useLaser = false;

    public int damageOverTime = 30;
    public float slowPercentage = 0.5f;

    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;

    [Header("User Setup")]
    public string enemyTag = "Enemy";

    public Transform partToRotate;
    public float turnspeed = 10f;

    public Transform firePoint;

    public AudioSource audioSource;

    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            //targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            if(useLaser)
            {
                if(lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    impactLight.enabled = false;
                    impactEffect.Stop();
                    audioSource.Stop();
                    animator.SetBool("IsAttacking", false);
                }
            }
            return;
        }
        
        LockOnTarget();

        if (useLaser)
        {
            Laser();
        }
        else
        {
            if (firecountdown <= 0f)
            {
                Shoot();
                firecountdown = 1f / firerate;
            }

            firecountdown -= Time.deltaTime;
        }


        
    }

    void LockOnTarget()
    {
        //target lock on
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnspeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Laser()
    {
        //targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        //targetEnemy.Slow(slowPercentage);

        if(!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactLight.enabled = true;
            impactEffect.Play();
            audioSource.Play();
            animator.SetBool("IsAttacking", true);


        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = firePoint.position - target.position;

        impactEffect.transform.position = target.position + dir.normalized;

        impactEffect.transform.rotation = Quaternion.LookRotation(dir);
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate  (bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if(bullet != null)
        {
            bullet.Seek(target);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
