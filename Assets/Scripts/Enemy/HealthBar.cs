using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	private float Maxvalue = 100f;
	public Slider healthBar;
	public Camera camera;
	public Transform target;
	public Vector3 offset;

	public void UpdateHealth(float Currentvalue)
	{
		healthBar.value = Currentvalue / Maxvalue;
	}

	void Update()
	{
		transform.position = target.position + offset;
	}
}
