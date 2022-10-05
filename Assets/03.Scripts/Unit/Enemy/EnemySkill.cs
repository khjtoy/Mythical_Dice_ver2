using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class EnemySkill
{
    public abstract void DoAttack(UnitMove unit, Action callback = null);

    public virtual void CrossAttack(Vector2Int pos, int damage)
    {
        MapController.Instance.Boom(pos, damage);
        for (int i = 1; i <= GameManager.Instance.Size; i++)
        {
            MapController.Instance.Boom(pos + Vector2Int.up * i, damage);
            MapController.Instance.Boom(pos + Vector2Int.down * i, damage);
            MapController.Instance.Boom(pos + Vector2Int.left * i, damage);
            MapController.Instance.Boom(pos + Vector2Int.right * i, damage);
        }
    }
    public virtual void RangeAttack(Vector2Int pos, int range, int damage)
    {
        for(int i = -range; i <= range; i++)
        {
            for (int j = -range; j <= range; j++)
            {
                MapController.Instance.Boom(pos + Vector2Int.up * i + Vector2Int.right * j, damage);
            }
        }
    }
}
