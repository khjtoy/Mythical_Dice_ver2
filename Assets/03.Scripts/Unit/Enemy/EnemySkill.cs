using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Win32.SafeHandles;
using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public abstract class EnemySkill
{

    public abstract void DoAttack(UnitMove unit, Action ani = null, Action callback = null);

    public virtual void LineAttack(int x, int y, int damage, Color diceColor)
    {
        int size = GameManager.Instance.Size;
        for(int i = 0; i < size; i++)
        {
            if (x != -1)
                MapController.Instance.Boom(x, i, damage, diceColor);
            if (y != -1)
                MapController.Instance.Boom(i, y, damage, diceColor);
        }
    }
    public virtual void CrossAttack(Vector2Int pos, int damage)
    {
        LineAttack(pos.x, pos.y, damage, Color.red);
    }
    public virtual void SquareRangeAttack(Vector2Int pos, int range, int damage)
    {
        for(int i = -range; i <= range; i++)
        {
            for (int j = -range; j <= range; j++)
            {
                MapController.Instance.Boom(pos + Vector2Int.up * i + Vector2Int.right * j, damage, Color.red);
            }
        }
    }
    public virtual void RangeWireAttack(Vector2Int pos, int range, int damage)
    {
        for (int i = -range; i <= range; i++)
        {
            for (int j = -range; j <= range; j++)
            {
                if (Mathf.Abs(i) == range || Mathf.Abs(j) == range)
                    MapController.Instance.Boom(pos + Vector2Int.up * i + Vector2Int.right * j, damage, Color.red);
            }
        }
    }
    public virtual void RhombusWireAttack(Vector2Int pos, int range, int damage, Color diceColor)
    {
        for (int i = -range; i <= range; i++)
        {
            int offset = range - Mathf.Abs(i);
            Vector2Int de = new Vector2Int(offset, i);
            MapController.Instance.Boom(pos + de, damage, diceColor);
        }
        for (int i = -range; i < range; i++)
        {
            int offset = -range + Mathf.Abs(i);
            Vector2Int de = new Vector2Int(offset, i);
            MapController.Instance.Boom(pos + de, damage, diceColor);
        }
    }
    protected void PushAttack(Vector2Int pos, Vector2Int direction, int damage)
    {
        Vector2Int temp = Vector2Int.zero;
        if (direction.x != 0)
        {
            for (int i = -1; i <= 1; i++)
            {
                temp = pos + new Vector2Int(direction.x, i);
                MapController.Instance.Boom(temp, damage, Color.red);
            }
        }
        else if(direction.y != 0)
        {
            for (int i = -1; i <= 1; i++)
            {
                temp = pos + new Vector2Int(i, direction.y);
                MapController.Instance.Boom(temp, damage, Color.red);
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
        for(int i = 0; i <= times; i++)
        {
            RhombusWireAttack(pos, i, damage, new Color(0.1f,0.5f,0.8f));
            yield return new WaitForSeconds(delay);
        }
    }

    protected IEnumerator LineWaveAttack(Vector2Int pos, int damage, float delay, Color diceColor)
    {
        int size = GameManager.Instance.Size - 1;
        for (int i = 0; i <= size; i++)
        {
            if(pos.x == 1)
                LineAttack(i, -1, damage, diceColor);
            if(pos.x == -1)
                LineAttack(size - i, -1, damage, diceColor);
            if(pos.y == 1)
                LineAttack(-1, i, damage,    diceColor);
            if(pos.y == -1)
                LineAttack(-1, size - i, damage, diceColor);
            yield return new WaitForSeconds(delay);
        }
    }
}
