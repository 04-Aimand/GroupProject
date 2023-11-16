using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    private EnemyScript enemy;

    public float speed = 70f;
    public float damage = 10;
    public GameObject effect;
    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        /*if(target == null)
        {
            Destroy(gameObject);
            return;
        }*/

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

    }

    void HitTarget()
    {
        enemy = target.GetComponent<EnemyScript>();
        enemy.TakeDamage(damage);

        GameObject effectIns = (GameObject)Instantiate(effect, transform.position, transform.rotation);
        Destroy(effectIns, 2f);

        Destroy(gameObject);
    }
}
