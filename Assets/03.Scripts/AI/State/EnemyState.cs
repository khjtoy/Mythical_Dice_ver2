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

    private void Awake()
    {
        if (Transitions.Count != transform.childCount)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                AITransition temp = transform.GetChild(i).GetComponent<AITransition>();
                if (Transitions.Contains(temp))
                    continue;
                Transitions.Add(temp);
            }
        }
        
    }
}
