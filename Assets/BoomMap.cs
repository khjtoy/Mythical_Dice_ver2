using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BoomMap : MonoSingleton<BoomMap>
{
	[Header("몇초 후 부서짐")]
	public float wait;

	public void Boom()
	{
		int brokeNum = GameManager.Instance.BossNum;
		for (int i = 0; i < GameManager.Instance.Height; i++)
		{
			for (int j = 0; j < GameManager.Instance.Width; j++)
			{
				if (MapController.Instance.dices[i][j].randoms == brokeNum)
				{
					MeshRenderer renderer = MapController.Instance.dices[i][j].GetComponent<MeshRenderer>();
					Sequence seq = DOTween.Sequence();
					seq.Append(renderer.material.DOColor(Color.red, 0.4f));
					seq.Append(renderer.material.DOColor(new Color(156, 146, 115) / 255, 0.3f));
					int n = i;
					int m = j;
					seq.AppendCallback(() =>
					{
						MapController.Instance.dices[n][m].transform.rotation = Quaternion.Euler(0, 0, 0);
						MapController.Instance.dices[n][m].isDiceDirecting = true;
						StartCoroutine(MapController.Instance.dices[n][m].BasicDiceNumSelect());
						seq.Kill();
					});
				}
			}
		}
	}

	public void Boom(int x, int y)
    {
		int brokeNum = GameManager.Instance.BossNum;
		if (MapController.Instance.dices[y][x].randoms == brokeNum)
		{
			MeshRenderer renderer = MapController.Instance.dices[y][x].GetComponent<MeshRenderer>();
			Sequence seq = DOTween.Sequence();
			seq.Append(renderer.material.DOColor(Color.red, 0.4f));
			seq.Append(renderer.material.DOColor(new Color(156, 146, 115) / 255, 0.3f));
			int n = y;
			int m = x;
			seq.AppendCallback(() =>
			{
				MapController.Instance.dices[n][m].transform.rotation = Quaternion.Euler(0, 0, 0);
				MapController.Instance.dices[n][m].isDiceDirecting = true;
				StartCoroutine(MapController.Instance.dices[n][m].BasicDiceNumSelect());
				seq.Kill();
			});
		}
	}
}
