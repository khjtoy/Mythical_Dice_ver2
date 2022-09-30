using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoSingleton<MapController>
{
	[SerializeField]
	private GameObject dicePrefabs;
	[SerializeField]
	private Transform root;

	public Transform Root { get { return root; } }

	[SerializeField]
	private float distance;

	public float Distance
	{
		get { return distance; }
	}

	private GameObject[][] diceObjectArr;
	public GameObject[][] DiceObject { get => diceObjectArr; }

	private int[][] mapNum = new int[3][];
	public int[][] MapNum
    {
		get { return mapNum; }
    }
	public DiceDirecting[][] Dices;
}
