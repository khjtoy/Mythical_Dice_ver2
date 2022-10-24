using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicDiceDirect : DiceSelect,Direct
{
	[SerializeField]
	private float wait = 0;
	protected override void Awake()
	{
		base.Awake();
	}
	public void Direction(DiceRotation diceRotation)
	{
		StartCoroutine(BasicDiceNumSelect(wait, diceRotation));
	}
	public IEnumerator BasicDiceNumSelect(float wait, DiceRotation rotation)
	{
		rotation.Rotation();
		transform.rotation = Quaternion.Euler(0, 0, 0);
		yield return new WaitForSeconds(wait);
		rotation.Rotation();
		DiceNumSelect();
	}

}
