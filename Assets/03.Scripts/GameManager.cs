using System;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
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
	public float Offset => 0.75f * (size - 1);

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

	private void Start()
	{
		DOTween.SetTweensCapacity(1000, 1000);
		if (SceneManager.sceneCount < 2)
		{
			SceneManager.LoadScene(0, LoadSceneMode.Additive);
		}
	}
}