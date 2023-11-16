using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyScript))]
public class Pathing : MonoBehaviour
{
    public float turnspeed;
    public EnemyScript enemy;
    private Transform Target;
    private int WaypointIndex;
    
    private void Start()
    {
        enemy = GetComponent<EnemyScript>();
        Target = Waypoints.points[0];
    }

    private void Update()
    {
        if (WaypointIndex <= Waypoints.points.Length - 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, Waypoints.points[WaypointIndex].transform.position, enemy.speed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, Waypoints.points[WaypointIndex].transform.position, enemy.speed * Time.deltaTime);
            if (transform.position == Waypoints.points[WaypointIndex].transform.position)
            {
                WaypointIndex += 1;
            }
            LockOnTarget();
        }
    }

    void LockOnTarget()
    {
        if (WaypointIndex >= Waypoints.points.Length-1)
        {
            return;
        }
        Target = Waypoints.points[WaypointIndex];
        //target lock on
        Vector3 dir = Target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnspeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

}
