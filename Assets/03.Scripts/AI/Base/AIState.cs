using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIState : MonoBehaviour
{
    [field: SerializeField]
    public EnemyAIState EnemyState { get; set; }

    [field:SerializeField]
    public EnemyMove EnemyMove { get; set; }

    [field: SerializeField] public List<AITransition> Transitions { get; set; } = new List<AITransition>();
    [field:SerializeField]
    public bool IsLoop { get; set; }
    public abstract void DoAction(Action callback = null);

    public void GetEnemyState(EnemyAIState state, EnemyMove move, bool isLoop)
    {
        EnemyState = state;
        EnemyMove = move;
        IsLoop = isLoop;
    }
}
