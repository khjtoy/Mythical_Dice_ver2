using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "SO/BossSO")]
public class BossSO : ScriptableObject
{
    public string Name = "";
    public int Hp = 0;
    public Sprite MainSprite = null;
    public Vector3 spriteOffset = Vector3.zero;
    public Vector3 spriteSize = Vector3.zero;
    public Material OutlineMat = null;
    public RuntimeAnimatorController Controller = null;
    public List<Condition> ToIdleCondition = new List<Condition>();
    public List<SkillSO> Skills = new List<SkillSO>();
    public SoundSO SkillSounds;

}