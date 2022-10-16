using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

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
}