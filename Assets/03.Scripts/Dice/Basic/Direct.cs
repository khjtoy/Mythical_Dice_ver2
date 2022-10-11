using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Direct
{
	[HideInInspector]
	public Dice dice { get; set; }

	public void Direction();
}
