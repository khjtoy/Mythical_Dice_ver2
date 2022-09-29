using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : AIState
{
    public override void DoAction(Action callback = null)
    {
        callback?.Invoke();
    }
}
