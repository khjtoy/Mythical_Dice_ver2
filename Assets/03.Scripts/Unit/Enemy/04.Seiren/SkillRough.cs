using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillRough : EnemySkill
{
    public override void DoAttack(UnitMove unit, Action callback = null)
    {
        Vector2Int pos = unit.GamePos;
        callback?.Invoke();
    }
}
