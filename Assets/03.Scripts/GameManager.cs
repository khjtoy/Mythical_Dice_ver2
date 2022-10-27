using System;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;
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
	[SerializeField] private RectTransform _hidePanel;
	public float Offset => 0.75f * (size - 1);

	[SerializeField] private List<BossSO> SO = new List<BossSO>();

	[SerializeField] private AudioMixer _mixer = null;

	public float Timer;
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

	private void Update()
	{
		if (StageStart)
			Timer += Time.deltaTime;
	}

	private void Awake()
	{
		int idx = PlayerPrefs.GetInt("NOWSTAGE", 1);
		bool isHard = PlayerPrefs.GetInt("HARD") == 1 ? true : false;
		idx = isHard ? idx + 3 : idx;
		idx = idx - 1;
		GenerateBoss(SO[idx]);
	}
	
	private void Start()
	{
		DOTween.SetTweensCapacity(1000, 1000);
		if (SceneManager.sceneCount < 2)
		{
			SceneManager.LoadScene(5, LoadSceneMode.Additive);
		}
	}
	public void SaveUserData(int id, float clearTime, bool isHard = false)
    {
		User user = DataManager.LoadJsonFile<User>(Application.dataPath + "/Save", "SAVEFILE");
		bool findStage = false;
		if (user.currentStage == id)
			user.clearStage = id+1;
		if (isHard)
        {
			user.userHardStages.ForEach(i =>
			{
				if(i.currentStage==id)
                {
					if (i.clearTime > clearTime)
						i.clearTime = (int)clearTime;

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
						i.clearTime = (int)clearTime;
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
	public void LoadStageScene(float timeToWait)
    {
		Sequence seq = DOTween.Sequence();
		seq.AppendInterval(timeToWait);
		seq.AppendCallback(() =>
		{
			Time.timeScale = 1;
		});
		seq.Append(_hidePanel.DOAnchorPosY(0, 1));
		seq.AppendCallback(() =>
		{
			SceneManager.LoadScene("Stage");
		});
    }

	public void GenerateBoss(BossSO bossSo)
	{
		size = bossSo.MapSize;
		GameObject go = new GameObject(bossSo.Name);
		go.tag = "Boss";
		go.transform.SetParent(_unitRootTrm);
		go.SetActive(false);

		go.AddComponent<AudioSource>().outputAudioMixerGroup = _mixer.FindMatchingGroups("Effect")[0];
		
		EnemyMove bossMove = go.AddComponent<EnemyMove>();
		bossMove.SkillAudioClips.AddRange(bossSo.SkillSounds.audioClips.ToList());
		EnemyStat bossStat = go.AddComponent<EnemyStat>();
		bossStat.InitStat(bossSo.Hp);
		
		go.AddComponent<UnitRender>();
		
		GameObject sprite_anchor = new GameObject("Sprite_Anchor");
		GameObject main_sprite = new GameObject("Main_Sprite");
		GameObject outline = new GameObject("Outline");
		
		sprite_anchor.transform.SetParent(go.transform);
		sprite_anchor.transform.localPosition = new Vector3(0, 0, -0.5f);
		sprite_anchor.transform.eulerAngles = new Vector3(45, 0, 0);
		sprite_anchor.transform.localScale = Vector3.one;
		
		main_sprite.transform.SetParent(sprite_anchor.transform);
		main_sprite.transform.eulerAngles = new Vector3(45, 0, 0);
		main_sprite.transform.localPosition = bossSo.spriteOffset;
		main_sprite.transform.localScale = bossSo.spriteSize;
		main_sprite.AddComponent<SpriteRenderer>().sprite = bossSo.MainSprite;
		main_sprite.AddComponent<Animator>().runtimeAnimatorController = bossSo.Controller;
		outline.transform.SetParent(main_sprite.transform);
		outline.transform.eulerAngles = new Vector3(45, 0, 0);
		outline.transform.localPosition = Vector3.zero;
		outline.transform.localScale = Vector3.one;
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

		GameObject IdleTransition = new GameObject("ToIDLE");
		IdleTransition.transform.SetParent(anyState.transform);
		AITransition ToIdleTransition = IdleTransition.AddComponent<AITransition>();
		ToIdleTransition.SetGoalState(IdleState);
		ToIdleTransition.SetCondition(bossSo.ToIdleCondition);
		
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
			toIdleTransition.SetGoalState(IdleState);
		}
		
		
		go.SetActive(true);
	}
}