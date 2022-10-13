using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/StageStatus")]
[Obsolete("이거 이제 안씀")]
public class StageSO : ScriptableObject
{
    
     public List<Stage> stages = new List<Stage>();
}
