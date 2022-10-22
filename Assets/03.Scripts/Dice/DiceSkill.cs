using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSkill : MonoSingleton<DiceSkill>
{
	public IEnumerator Spread(int x, int y, float speed = 0.1f)
	{
        for(int l = 0; l<GameManager.Instance.Size; l++)
		{
            for (int i = -l; i <= l; i++)
            {
                for (int j = -l; j <= l; j++)
                {
                    if (Mathf.Abs(i) == l || Mathf.Abs(j) == l)
                        MapController.Instance.Boom(x + i, y * j, MapController.Instance.dices[x, y].DiceSelect.Randoms);
                    yield return new WaitForSeconds(speed);
                }
            }
        }
    }
}
