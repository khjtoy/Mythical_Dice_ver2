using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UpDownDirect : Dice, Direct
{
	[SerializeField]
	private float upSecound;
	[SerializeField]
	private float downSecound;

	[SerializeField]
	private float upPos;

	//[SerializeField]
	//private DiceRotation diceRotation;

	protected override void Awake()
	{
		base.Awake();
		dice.directs.Add(this.GetType(), this.GetComponent<Direct>());
	}
	public void Direction(DiceRotation diceRotation)
	{
		diceRotation.Rotation();
		Sequence sequence = DOTween.Sequence();
		sequence = DOTween.Sequence();
		sequence.Append(this.gameObject.transform.DOMoveY(upPos, upSecound));
		sequence.Append(this.gameObject.transform.DOMoveY(0f, downSecound));
		sequence.AppendCallback(() => {
			DiceNumSelect();
			diceRotation.Rotation();
		});
	}
}
