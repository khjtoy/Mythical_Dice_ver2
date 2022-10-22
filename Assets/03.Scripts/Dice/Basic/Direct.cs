using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Direct
{
	public void Direction(DiceRotation diceRotation);

	public Dice dice { get; set; }
}
