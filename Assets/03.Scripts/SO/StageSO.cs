using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "SO/StageStatus")]
public class StageSO : ScriptableObject
{
     public List<Stage> stages = new List<Stage>();
}
