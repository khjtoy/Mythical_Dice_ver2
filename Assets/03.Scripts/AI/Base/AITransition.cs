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
}
