using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITransition : MonoBehaviour
{
    public AIState StartState = null;
    public List<AICondition> PositiveCondition = new List<AICondition>();
    public List<AICondition> NegativeCondition = new List<AICondition>();
    public AIState goalState = null;
    public bool IsPositiveAnd = false;
    public bool IsNegativeAnd = false;

    public void SetGoalState(AIState state)
    {
        goalState = state;
    }

    public void SetIdleCondition(EnemyState idleState)
    {
        SetGoalState(idleState);
    }

    public void SetDeathCondition(EnemyStat stat)
    {
        DeathCondition condition = gameObject.AddComponent<DeathCondition>();
        condition.EnemyStat = stat;
        PositiveCondition.Add(condition);
    }

    public AICondition SetCondition(Conditions condition)
    {
        switch (condition)
        {   
            case Conditions.IDLE:
                return gameObject.AddComponent<PlayerSkillCondition>();
            case Conditions.TIME:
                return gameObject.AddComponent<TimeCondition>();
            case Conditions.RANGEDETECT:
                return gameObject.AddComponent<RangeDetectCondition>();
            case Conditions.DIRECTDIRECTION:
                return gameObject.AddComponent<DirectDirectionCondition>();
        }

        return null;
    }
    public void SetCondition(SkillSO skill)
    {
        
        foreach (var condition in skill.FromIdleCondition)
        {
            AICondition currentCondition = null;
            currentCondition = SetCondition(condition.ConditionEnum);
            currentCondition.SetParam(condition.Parameter);
            if (condition.IsPositive)
            {
                PositiveCondition.Add(currentCondition);
            }
            else
            {
                NegativeCondition.Add(currentCondition);
            }
        }

        IsPositiveAnd = skill.IsPositiveAnd;
        IsNegativeAnd = skill.IsNegativeAnd;
    }
    
    public void SetCondition(List<Condition> conditions)
    {
        
        foreach (var condition in conditions)
        {
            AICondition currentCondition = null;
            currentCondition = SetCondition(condition.ConditionEnum);
            currentCondition.SetParam(condition.Parameter);
            if (condition.IsPositive)
            {
                PositiveCondition.Add(currentCondition);
            }
            else
            {
                NegativeCondition.Add(currentCondition);
            }
        }

        IsPositiveAnd = true;
        IsNegativeAnd = true;
    }
}
