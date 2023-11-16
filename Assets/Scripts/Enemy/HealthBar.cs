using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	private float Maxvalue = 100f;
	
	public Camera camera;
	public Transform target;
	public Vector3 offset;

	public void UpdateHealth(float Currentvalue)
	{
		
	}

	void Update()
	{
		transform.position = target.position + offset;
	}
}
