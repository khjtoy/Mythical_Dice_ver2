using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Dice:MonoBehaviour
{
	public Dictionary<System.Type, Direct> directs = new Dictionary<System.Type, Direct>();
	public Dictionary<System.Type, DiceRotation> rotation = new Dictionary<System.Type, DiceRotation>();

	public DiceSelect DiceSelect => diceSelect;
	private DiceSelect diceSelect;

	private void Awake()
	{
		diceSelect = GetComponent<DiceSelect>();
		Direct[] component = GetComponents<Direct>();
		DiceRotation[] rotations = GetComponents<DiceRotation>();
		foreach (Direct type in component)
		{
			directs.Add(type.GetType(), type);
		}
		foreach (DiceRotation type in rotations)
		{
			rotation.Add(type.GetType(), type);
		}
	}

	public void Direct(System.Type diceType, System.Type rotationType)
	{
		directs[diceType].Direction(rotation[rotationType]);
	}
}
