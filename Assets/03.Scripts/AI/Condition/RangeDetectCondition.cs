using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeDetectCondition : AICondition
{
    [SerializeField]
    Transform baseTrm = null;
    public override bool Result()
    {
        return true;
    }
}
