using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
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
	[SerializeField] private UnitAnimation _animation = new UnitAnimation();
	private Dictionary<EnemyAIState, EnemySkill> _enemySkillDict = new Dictionary<EnemyAIState, EnemySkill>();
	[SerializeField] private List<EnemySkill> enemySkills = new List<EnemySkill>();
	private List<int> hashes = new List<int>();
	
	
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

		for (var i = EnemyAIState.ANY; i < EnemyAIState.COUNT; i++)
		{
			hashes.Add(Animator.StringToHash(i.ToString()));
		}

		for (int i = 0; i < _enemySkillDict.Count; i++)
		{
			//_enemySkillDict[(EnemyAIState)i].audioSource = audioSource;
		}

		float offset = GameManager.Instance.Offset;
		transform.localPosition = new Vector3(offset, 0, offset);
		WorldPos = transform.localPosition;
	}

	protected override void Start()
	{
		base.Start();
		_animation.SetAnimator(transform.GetChild(0).GetChild(0).GetComponent<Animator>());
	}

	public void  DoSkill(EnemyAIState state, Action callback = null)
	{
		if (state == EnemyAIState.ANY || state == EnemyAIState.IDLE)
		{
			render.SetSortingLayer();
			callback?.Invoke();
			return;
		}
		Action ani = () =>
		{
			_animation.PlayAnimator(hashes[(int)state]);
		};
		_enemySkillDict[state].DoAttack(this, ani, callback);
	}

	public override void Translate(Vector3 pos)
	{

	}
}
