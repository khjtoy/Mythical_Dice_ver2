using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BloodControl : MonoBehaviour
{
	private Sequence seq;
	[SerializeField]
	private Image attackBlood;

	[SerializeField]
	private Image blood;
	public void BloodSet(float hp, float mHp)
	{
		//Color a = attackBlood.color;
		//a.a = 0.5f - (0.5f * ((float)(hp / mHp * 100) / 100));
		//attackBlood.color = a;
	}
	public void BloodFade(int damage)
	{
		seq.Kill();
		seq = DOTween.Sequence();
		float d = 0f;
		float percent = (float)damage / 6 * 100;
		if (percent == 16.67f)
			d = 0.2f;
		seq.Append(blood.DOFade(0.7f * (percent / 100) + d, 0.5f).OnComplete(() => blood.DOFade(0, 0.5f)));
		seq.AppendCallback(() => seq.Kill());
	}
}
