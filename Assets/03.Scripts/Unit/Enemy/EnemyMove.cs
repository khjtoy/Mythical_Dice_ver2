using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyAIState{
	ANY,
    IDLE,
    SLIME,
    STAMP,
    JUMP,
    DASH,
    SWIM,
    ROUGH,
    SILENT,
    DEATH,
    COUNT
}
public class EnemyMove : UnitMove
{
	private Dictionary<EnemyAIState, EnemySkill> _enemySkillDict = new Dictionary<EnemyAIState, EnemySkill>();
	[SerializeField] private List<EnemySkill> enemySkills = new List<EnemySkill>();


	public void Awake()
	{
		_enemySkillDict.Add(EnemyAIState.IDLE, null);
		_enemySkillDict.Add(EnemyAIState.SLIME, new SkillSlime());
		_enemySkillDict.Add(EnemyAIState.STAMP, new SkillStamp());
		_enemySkillDict.Add(EnemyAIState.JUMP, new SkillJump());
		_enemySkillDict.Add(EnemyAIState.DASH, new SkillDash());
		_enemySkillDict.Add(EnemyAIState.SWIM, new SkillSwim());
		_enemySkillDict.Add(EnemyAIState.ROUGH, new SkillRough());
		_enemySkillDict.Add(EnemyAIState.SILENT, new SkillSilent());
		_enemySkillDict.Add(EnemyAIState.DEATH, new DeathState());

		for (int i = 0; i < _enemySkillDict.Count; i++)
		{
			//_enemySkillDict[(EnemyAIState)i].audioSource = audioSource;
		}

		float offset = GameManager.Instance.Offset;
		transform.localPosition = new Vector3(offset, 0, offset);
		WorldPos = transform.localPosition;
	}
	public void  DoSkill(EnemyAIState state, Action callback = null)
	{
		if (state == EnemyAIState.ANY || state == EnemyAIState.IDLE)
		{
			callback?.Invoke();
			return;
		}
		_enemySkillDict[state].DoAttack(this, callback);
	}

	public override void Translate(Vector3 pos)
	{

	}
}
