using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GameManager.instance.TakeDamage(10);
            GameManager.instance.EnemyDefeated();
            Destroy(collision.gameObject);
        }
    }
}
