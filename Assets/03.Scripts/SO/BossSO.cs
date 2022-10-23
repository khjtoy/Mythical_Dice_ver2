using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "SO/BossSO")]
public class BossSO : ScriptableObject
{
    public string Name = "";
    public int Hp = 0;
    public Sprite MainSprite = null;
    public Material OutlineMat = null;
    public RuntimeAnimatorController Controller = null;
    public List<SkillSO> Skills = new List<SkillSO>();

}