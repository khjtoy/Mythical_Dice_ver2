using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : EnemySkill
{
    public override void DoAttack(UnitMove unit, Action ani = null, Action callback = null)
    {
        ani?.Invoke();
    }
}
