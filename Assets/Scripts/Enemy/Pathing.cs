using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyScript))]
public class Pathing : MonoBehaviour
{
    public Transform[] waypoints;
    private Transform nextWaypoint;

    private EnemyScript enemy;

    public float turnspeed;

    private int WaypointIndex = 0;
    
    private void Start()
    {
        enemy = GetComponent<EnemyScript>();
        transform.position = waypoints[WaypointIndex].transform.position;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (WaypointIndex <= waypoints.Length - 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[WaypointIndex].transform.position, enemy.speed * Time.deltaTime);
            if (transform.position == waypoints[WaypointIndex].transform.position)
            {
                WaypointIndex += 1;
            }
            LockOnTarget();
        }
    }

    void LockOnTarget()
    {
        if(WaypointIndex >= waypoints.Length)
        {
            return;
        }
        nextWaypoint = waypoints[WaypointIndex];
        //target lock on
        Vector3 dir = nextWaypoint.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnspeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
}
