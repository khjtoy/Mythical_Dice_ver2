using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectDirectionCondition : AICondition
{
    public override bool Result()
    {
        Vector2Int pos = Define.EnemyMove.GamePos;
        Vector2Int playerPos = Define.PlayerMove.GamePos;
        int size = GameManager.Instance.Size;
        if(pos.x == playerPos.x)
            return true;
        if (pos.y == playerPos.y)
            return true;
        return false;

    }
}
