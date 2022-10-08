using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : AIState
{
    public override void DoAction(Action callback = null)
    {
        EnemyMove.DoSkill(EnemyState, callback);
    }
}
