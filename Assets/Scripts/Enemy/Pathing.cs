using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyScript))]
public class Pathing : MonoBehaviour
{
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
        Vector3 dir = Target.position - transform.position;
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

        if (Vector2.Distance(transform.position, Target.position) <= 2.0f)
        {
            GetNextWaypoint();
        }
    }
    void GetNextWaypoint()
    {
        if(WaypointIndex >= Waypoints.points.Length -1)
        {
            return;
        }

        WaypointIndex++;
        Target = Waypoints.points[WaypointIndex];
    }

}
