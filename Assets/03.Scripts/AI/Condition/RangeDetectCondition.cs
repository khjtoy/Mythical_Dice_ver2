using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeDetectCondition : AICondition
{
    [SerializeField]
    private int range = 0;
    public override bool Result()
    {
        Vector2Int pos = Define.EnemyMove.GamePos;
        Vector2Int playerPos = Define.PlayerMove.GamePos;
        for (int i = -range; i <= range; i++)
            for (int j = -range; j <= range; j++)
            {
                if(pos + new Vector2Int(i, j) == playerPos)
                    return true;
            }
        return false;
    }

    public override void SetParam(float param)
    {
        range = (int)param;
    }
}
