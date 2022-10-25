using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillCondition : AICondition
{

    public override bool Result()
    {
        return Define.IsUsingSkill;
    }

    public override void SetParam(float param)
    {
        
    }
}
