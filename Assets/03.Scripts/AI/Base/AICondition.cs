using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Conditions
{
    IDLE,
    TIME,
    RANGEDETECT,
    DIRECTDIRECTION,
    DEATH,
    ISNOTLOAD,
}
public abstract class AICondition : MonoBehaviour
{
    public abstract bool Result();
    public abstract void SetParam(float param);
}

[System.Serializable]
public class Condition
{
    public Conditions ConditionEnum;
    public float Parameter = 0;
    public bool IsPositive = true;
}
