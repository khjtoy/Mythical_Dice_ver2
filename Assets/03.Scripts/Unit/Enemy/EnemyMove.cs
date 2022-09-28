using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyAIState{
    IDLE,
    STAMP,
    COUNT
}
public class EnemyMove : MonoBehaviour
{
    private Dictionary<EnemyAIState, EnemySkill> _enemySkillDict = new Dictionary<EnemyAIState, EnemySkill>();
    [SerializeField] private List<EnemySkill> enemySkills = new List<EnemySkill>();

    public void Awake()
    {
        _enemySkillDict.Add(EnemyAIState.IDLE, null);
        _enemySkillDict.Add(EnemyAIState.STAMP, new SkillStamp());
    }
    public void DoSkill(EnemyAIState state)
    {
        _enemySkillDict[state].DoAttack();
    }
}
