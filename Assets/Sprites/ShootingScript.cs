using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public AudioSource AS;
    public AudioClip StartingUp, Shooting, Upgrade;
    public Transform ShootingPoint;
    public GameObject FireProjectile;
    void Start()
    {
        AS.clip = StartingUp;
        AS.Play();
    }


    public void Update()
    {
        if (InRadius)
        {
            Shoot();
        }
    }
    private IEnumerator Shoot()
    {
        Instantiate(FireProjectile, ShootingPoint.transform.position, ShootingPoint.transform.rotation);
        yield return new WaitForSeconds(3f);
    }
}
