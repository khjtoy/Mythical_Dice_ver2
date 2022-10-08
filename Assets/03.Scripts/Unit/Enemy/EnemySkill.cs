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
    public virtual void SquareRangeAttack(Vector2Int pos, int range, int damage)
    {
        for(int i = -range; i <= range; i++)
        {
            for (int j = -range; j <= range; j++)
            {
                MapController.Instance.Boom(pos + Vector2Int.up * i + Vector2Int.right * j, damage);
            }
        }
    }
    public virtual void RangeWireAttack(Vector2Int pos, int range, int damage)
    {
        for (int i = -range; i <= range; i++)
        {
            for (int j = -range; j <= range; j++)
            {
                if(Mathf.Abs(i) == range || Mathf.Abs(j) == range)
                    MapController.Instance.Boom(pos + Vector2Int.up * i + Vector2Int.right * j, damage);
            }
        }
    }
    public virtual void RhombusWireAttack(Vector2Int pos, int range, int damage)
    {
        for (int i = -range; i <= range; i++)
        {
            int offset = range - Mathf.Abs(i);
            Vector2Int de = new Vector2Int(offset, i);
            MapController.Instance.Boom(pos + de, 1);
        }
        for (int i = -range; i < range; i++)
        {
            int offset = -range + Mathf.Abs(i);
            Vector2Int de = new Vector2Int(offset, i);
            MapController.Instance.Boom(pos + de, 1);
        }
    }
    protected void PushAttack(Vector2Int pos, Vector2Int direction, int damage)
    {
        Vector2Int temp = Vector2Int.zero;
        for(int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                temp = pos + new Vector2Int(j, i);
                if (temp.x == pos.x - direction.x && temp.x != pos.x)
                    continue;
                if (temp.y == pos.y - direction.y && temp.y != pos.y)
                    continue;
                MapController.Instance.Boom(temp, damage);

            }
        }
    }

    protected IEnumerator WaveAttackCoroutine(Vector2Int pos, int damage, float delay)
    {
        for (int i = 0; i <= GameManager.Instance.Size; i++)
        {
            RangeWireAttack(pos, i, damage);
            yield return new WaitForSeconds(delay);
        }
    }

    protected IEnumerator SwimAttackCoroutine(Vector2Int pos, int times, float delay, int damage)
    {
        for(int i = 1; i <= times; i++)
        {
            RhombusWireAttack(pos, i, damage);
            yield return new WaitForSeconds(delay);
        }
    }
}
