using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Vector3 offset;
    public Transform player;
    public float smoothSpeed;

    void FixedUpdate()
    {
        Vector3 finalPosition = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, finalPosition, smoothSpeed * Time.deltaTime);
    }
}
