using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class User
{
    public int clearStage = 1;
    public int currentStage = 1;
    public List<UserStageVO> userStages;
    public List<UserStageVO> userHardStages;
}
