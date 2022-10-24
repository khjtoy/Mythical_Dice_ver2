using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/SkillSO")]
public class SkillSO : ScriptableObject
{
    public EnemyAIState State;
    public List<Condition> FromIdleCondition = new List<Condition>();
    public List<Condition> ToIdleCondition = new List<Condition>();
    public bool IsLoop = false;
    public bool IsPositiveAnd = false;
    public bool IsNegativeAnd = false;
}
