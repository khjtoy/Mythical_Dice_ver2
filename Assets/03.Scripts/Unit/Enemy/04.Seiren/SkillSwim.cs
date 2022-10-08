using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSwim : EnemySkill
{
    public override void DoAttack(UnitMove unit, Action callback = null)
    {
        unit.StartCoroutine(SwimAttackCoroutine(unit.GamePos, 8, 0.4f, 1));
        callback?.Invoke();
    }
}
