using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRotation : DiceSelect, DiceRotation
{
	public float speed;

	private bool isRotation;
	private void Update()
	{
		if(isRotation)
			transform.localRotation = Quaternion.Euler(transform.localEulerAngles + new Vector3(0, 1, 1) * Time.deltaTime * speed);
	}
	public void Rotation()
	{
		isRotation = !isRotation;
	}
}
