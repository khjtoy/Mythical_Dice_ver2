using System;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Jobs;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
	[Header("¸Ê Å©±â ÁöÁ¤")]
	[SerializeField]
	private int size;

	[field: SerializeField]
	public int BossNum { get; set; }

	public int StageNum;

	public bool StageStart;

	public bool thirdTutorial = false;


	[SerializeField]
	private MapController mapController;

	[SerializeField] private Transform _unitRootTrm;
	public float Offset => 0.75f * (size - 1);

	[SerializeField] private BossSO slimeSO = null;

	public int Size

	{
		get
		{
			return size;
		}
	}
	
	protected override void Init()
	{
	}

	private void Awake()
	{
		GenerateBoss(slimeSO);
		
	}
	
	private void Start()
	{
		DOTween.SetTweensCapacity(1000, 1000);
		if (SceneManager.sceneCount < 2)
		{
			SceneManager.LoadScene(0, LoadSceneMode.Additive);
		}
	}
	public void SaveUserData(int id, float clearTime, bool isHard = false)
    {
		User user = DataManager.LoadJsonFile<User>(Application.dataPath + "/Save", "SAVEFILE");
		bool findStage = false;
		if(!isHard)
        {
			user.userHardStages.ForEach(i =>
			{
				if(i.currentStage==id)
                {
					if (i.clearTime > clearTime)
						i.clearTime = clearTime;
					i.clearCount++;
					findStage = true;

				}
			});
        }
		else
        {
			user.userStages.ForEach(i =>
			{
				if (i.currentStage == id)
				{
					if (i.clearTime > clearTime)
						i.clearTime = clearTime;
					i.clearCount++;
					findStage = true;
				}
			});
		}

		if(findStage==false)
        {
			UserStageVO vo = new UserStageVO
			{
				currentStage = id,
				clearTime = clearTime,
				clearCount = 1
			};
			if (isHard)
				user.userHardStages.Add(vo);
			else
				user.userStages.Add(vo);
		}

		string str = DataManager.ObjectToJson(user);
		DataManager.SaveJsonFile(Application.dataPath + "/Save", "SAVEFILE", str);
    }

	public void GenerateBoss(BossSO bossSo)
	{
		GameObject go = new GameObject(bossSo.Name);
		go.tag = "Boss";
		go.transform.SetParent(_unitRootTrm);
		go.SetActive(false);

		EnemyMove bossMove = go.AddComponent<EnemyMove>();
		EnemyStat bossStat = go.AddComponent<EnemyStat>();
		bossStat.InitStat(bossSo.Hp);
		
		go.AddComponent<UnitRender>();
		
		GameObject sprite_anchor = new GameObject("Sprite_Anchor");
		GameObject main_sprite = new GameObject("Main_Sprite");
		GameObject outline = new GameObject("Outline");
		
		sprite_anchor.transform.SetParent(go.transform);
		sprite_anchor.transform.localPosition = Vector3.zero;
		sprite_anchor.transform.eulerAngles = new Vector3(45, 0, 0);
		sprite_anchor.transform.localScale = Vector3.one;
		
		main_sprite.transform.SetParent(sprite_anchor.transform);
		main_sprite.transform.eulerAngles = new Vector3(45, 0, 0);
		//TODO Get Transform Properties
		main_sprite.AddComponent<SpriteRenderer>().sprite = bossSo.MainSprite;
		main_sprite.AddComponent<Animator>().runtimeAnimatorController = bossSo.Controller;
		outline.transform.SetParent(main_sprite.transform);
		outline.transform.eulerAngles = new Vector3(45, 0, 0);
		SpriteRenderer outlineRenderer = outline.AddComponent<SpriteRenderer>();
		outlineRenderer.sprite = bossSo.MainSprite;
		//outlineRenderer.material = bossSo.OutlineMat;


		GameObject AI = new GameObject("AI");
		AIHead head = AI.AddComponent<AIHead>();
		GameObject State = new GameObject("State");
		
		AI.transform.SetParent(go.transform);
		State.transform.SetParent(AI.transform);

		GameObject anyState = new GameObject("ANY");
		anyState.transform.SetParent(State.transform);
		EnemyState AnyState = anyState.AddComponent<EnemyState>();
		AnyState.GetEnemyState(EnemyAIState.ANY, bossMove, false);
		
		GameObject deathState = new GameObject("DEATH");
		deathState.transform.SetParent(State.transform);
		EnemyState DeathState = deathState.AddComponent<EnemyState>();
		DeathState.GetEnemyState(EnemyAIState.DEATH, bossMove, false);
		
		GameObject deathTransition = new GameObject("ToDEATH");
		deathTransition.transform.SetParent(anyState.transform);
		AITransition toDeathTransition = deathTransition.AddComponent<AITransition>();
		toDeathTransition.SetGoalState(DeathState);
		toDeathTransition.SetDeathCondition(bossStat);
		
		GameObject idleState = new GameObject("IDLE");
		idleState.transform.SetParent(State.transform);
		EnemyState IdleState = idleState.AddComponent<EnemyState>();
		IdleState.GetEnemyState(EnemyAIState.IDLE, bossMove, false);

		head.Init(IdleState);
		foreach (var state in bossSo.Skills)
		{
			GameObject skillState = new GameObject(state.State.ToString());
			skillState.transform.SetParent(State.transform);
			
			EnemyState states = skillState.AddComponent<EnemyState>();
			states.GetEnemyState(state.State, bossMove, state.IsLoop);
			
			GameObject transition = new GameObject("To" + state.State.ToString());
			transition.transform.SetParent(idleState.transform);
			AITransition toTransition = transition.AddComponent<AITransition>();
			toTransition.SetGoalState(states);
			toTransition.SetCondition(state);
			
			GameObject idleTransition = new GameObject("ToIDLE");
			idleTransition.transform.SetParent(skillState.transform);
			AITransition toIdleTransition = idleTransition.AddComponent<AITransition>();
			toIdleTransition.SetIdleCondition(IdleState);
		}
		
		
		go.SetActive(true);
	}
}