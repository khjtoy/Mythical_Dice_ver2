using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BasicDirect : MonoBehaviour, Direct
{
	[SerializeField]
	private float upSecound;
	[SerializeField]
	private float downSecound;

	[SerializeField]
	private float upPos;

	[SerializeField]
	private DiceRotation basicRotation;

	public Dice dice { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

	private void Awake()
	{
		//dice.GetComponent<Dice>();
		dice.Directs.Add(this.GetType(), this.GetComponent<Direct>());
	}

	public void Direction()
	{
		basicRotation.Rotation();
		Sequence sequence = DOTween.Sequence();
		sequence = DOTween.Sequence();
		sequence.Append(this.gameObject.transform.DOMoveY(upPos, upSecound));
		sequence.Append(this.gameObject.transform.DOMoveY(0f, downSecound));
		sequence.AppendCallback(() => {
			basicRotation.Rotation();
			dice.DiceNumSelect();
		});
	}
}
