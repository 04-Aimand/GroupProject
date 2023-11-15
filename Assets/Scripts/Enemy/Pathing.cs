using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathing : MonoBehaviour
{
    public Transform[] waypoints;

    public float moveSpeed;

    private int WaypointIndex = 0;

    void Start()
    {
        transform.position = waypoints[WaypointIndex].transform.position;
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (WaypointIndex <= waypoints.Length - 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[WaypointIndex].transform.position, moveSpeed * Time.deltaTime);
            if (transform.position == waypoints[WaypointIndex].transform.position)
            {
                WaypointIndex += 1;
            }
        }
    }
}
