using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSilent : EnemySkill
{
    public override void DoAttack(UnitMove unit, Action callback = null)
    {
        callback?.Invoke();
    }
}
