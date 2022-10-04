using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class EnemySkill
{
    public abstract void DoAttack(UnitMove unit, Action callback = null);
}
