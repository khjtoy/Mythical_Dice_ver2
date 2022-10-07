using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyAIState{
    IDLE,
    STAMP,
    JUMP,
    Dash,
    COUNT
}
public class EnemyMove : UnitMove
{
    private Dictionary<EnemyAIState, EnemySkill> _enemySkillDict = new Dictionary<EnemyAIState, EnemySkill>();
    [SerializeField] private List<EnemySkill> enemySkills = new List<EnemySkill>();

    public void Awake()
    {
        _enemySkillDict.Add(EnemyAIState.IDLE, null);
        _enemySkillDict.Add(EnemyAIState.STAMP, new SkillStamp());
        _enemySkillDict.Add(EnemyAIState.JUMP, new SkillJump());
        _enemySkillDict.Add(EnemyAIState.Dash, new SkillDash());
    }
    public void DoSkill(EnemyAIState state, Action callback = null)
    {
        Debug.Log(state);
        if(state == EnemyAIState.IDLE)
        {
            callback?.Invoke();
            return;
        }
        _enemySkillDict[state].DoAttack(this, callback);
    }

    public override void Translate(Vector3 pos)
    {

    }
}
