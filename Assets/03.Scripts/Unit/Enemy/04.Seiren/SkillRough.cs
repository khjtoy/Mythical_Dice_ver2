using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillRough : EnemySkill
{
    public override void DoAttack(UnitMove unit, Action callback = null)
    {
        Vector2Int pos = unit.GamePos;
        Vector2Int enemyPos = Define.PlayerMove.GamePos;
        
        Vector3 direction = ((Vector2)enemyPos - pos).normalized;
        Vector2Int dir = new Vector2Int(Mathf.CeilToInt(direction.x), Mathf.CeilToInt(direction.y));

        unit.StartCoroutine(LineWaveAttack(dir, 1, 0.2f));

        callback?.Invoke();
    }
}
