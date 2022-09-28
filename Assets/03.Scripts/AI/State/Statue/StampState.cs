using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampState : AIState
{
    public override void DoAction()
    {
        EnemyMove.DoSkill(EnemyState);
    }
}
