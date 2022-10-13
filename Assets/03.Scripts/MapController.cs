using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

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

	private GameObject[,] diceObjectArr;
	public GameObject[,] DiceObject { get => diceObjectArr; }

	private int[,] mapNum;
	public int[,] MapNum
	{
		get { return mapNum; }
		set { mapNum = value; }
	}

	public DiceDirecting[,] dices;

	[SerializeField]
	private float floorChangeTime;
	[Header("몇초 후 부서짐")]
	public float wait;

	private Vector2 condition;

	public bool isDual;
	public bool XAxis;
	public bool isDown;
	public bool isLeft;
	private void Start()
	{
		InitMap();
	}
	public void InitMap()
	{
		diceObjectArr = new GameObject[GameManager.Instance.Size, GameManager.Instance.Size];
		dices = new DiceDirecting[GameManager.Instance.Size, GameManager.Instance.Size];
		mapNum = new int[GameManager.Instance.Size, GameManager.Instance.Size];
		SpawnMap();
	}
	private void SpawnMap()
	{
		for (int y = 0; y < GameManager.Instance.Size; y++)
		{
			for (int x = 0; x < GameManager.Instance.Size; x++)
			{
				MapInitSet(y, x);
			}
		}
		FloorInit();
	}

	private void MapInitSet(int y, int x)
	{
		ref GameObject diceObject = ref diceObjectArr[y, x];
		dices[y, x] = Instantiate(dicePrefabs, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<DiceDirecting>();
		dices[y, x].Pos = new Vector2Int(x, y);
		dices[y, x].transform.SetParent(root);

		diceObject = dices[y, x].gameObject;
		float posX = x * distance - GameManager.Instance.Offset;
		float posZ = y * distance - GameManager.Instance.Offset;
		diceObject.transform.localPosition = new Vector3(posX, 0, posZ);

		diceObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
		diceObject.transform.localScale = new Vector3(1, 1, 1);

	}

	private void FloorInit(int x = 0, int y = 0, bool isfirst = false)
	{
		if ((y < 0 || y >= GameManager.Instance.Size || x < 0 || x >= GameManager.Instance.Size) && isDual)
		{
			return;
		}

		if (isDown && !isfirst)
		{
			y = GameManager.Instance.Size - 1;
			condition = new Vector2(condition.x, 0);
		}

		if (!isDown && !isfirst)
			condition = new Vector2(condition.x, GameManager.Instance.Size - 1);

		if (isLeft && !isfirst)
		{
			x = GameManager.Instance.Size - 1;
			condition = new Vector2(0, condition.y);
		}
		if (!isLeft && !isfirst)
			condition = new Vector2(GameManager.Instance.Size - 1, condition.y);

		if (!isDual && GameManager.Instance.Size % 2 == 0 && !isfirst)
			condition = new Vector2(condition.x, 0);

		if (!isDual)
		{
			if (isDown && y < 0)
			{
				y = 0;
				x += isLeft ? -1 : 1;
				isDown = false;
			}
			else
			{
				if (y >= GameManager.Instance.Size)
				{
					y = GameManager.Instance.Size - 1;
					x += isLeft ? -1 : 1;
					isDown = true;
				}
			}

			if (isLeft && x < 0)
			{
				x = 0;
				y += isDown ? -1 : 1;
				isLeft = false;
			}
			else
			{
				if (x >= GameManager.Instance.Size)
				{
					x = GameManager.Instance.Size - 1;
					y += isDown ? -1 : 1;
					isLeft = true;
				}
			}
		}



		isfirst = true;
		dices[y, x].DiceStart();
		StartCoroutine(WaitFloor(x, y, isfirst));
	}

	private IEnumerator WaitFloor(int x, int y, bool isfirst)
	{
		yield return new WaitForSeconds(floorChangeTime);
		dices[y, x].DiceNumSelect();
		mapNum[y, x] = dices[y, x].Randoms;

		if (!isDual)
		{
			if (x == condition.x && y == condition.y)
			{
				//Boom();
				GameManager.Instance.StageStart = true;
				yield break;
			}

			if (XAxis)
				x += isLeft == true ? -1 : 1;
			else
				y += isDown == true ? -1 : 1;

			FloorInit(x, y, isfirst);
		}
		else
		{
			if (condition.x == x && condition.y == y)
			{
				GameManager.Instance.StageStart = true;
				yield break;
			}

			if (isLeft)
				FloorInit(x - 1, y, isfirst);
			else
				FloorInit(x + 1, y, isfirst);

			if (isDown)
				FloorInit(x, y - 1, isfirst);
			else
				FloorInit(x, y + 1, isfirst);
		}
	}
	public void BoomSameNum()
	{
		int brokeNum = GameManager.Instance.BossNum;
		for (int i = 0; i < GameManager.Instance.Size; i++)
		{
			for (int j = 0; j < GameManager.Instance.Size; j++)
			{
				if (dices[i, j].Randoms == brokeNum)
				{
					MeshRenderer renderer = dices[i, j].GetComponent<MeshRenderer>();
					Debug.Log(renderer.GetInstanceID());
					Sequence seq = DOTween.Sequence();
					seq.Append(renderer.material.DOColor(Color.red * 0.8f, 0.3f));
					seq.Append(renderer.material.DOColor(new Color(38, 8, 0) / 255, 0.2f));
					int n = i;
					int m = j;
					seq.AppendCallback(() =>
					{
						StartCoroutine(dices[n, m].BasicDiceNumSelect());
						seq.Kill();
					});
				}
			}
		}
	}

	public void BoomSameNum(int value)
	{		
		GameManager.Instance.BossNum = value;
		int brokeNum = GameManager.Instance.BossNum;
		int size = GameManager.Instance.Size;
		int sizeSqr = size * size;
		for (int i = 0; i < sizeSqr; i++)
		{
			int x = i / size;
			int y = i % size;
			if(mapNum[y, x] == brokeNum)
				Boom(x, y);
		}
	}

	public void Boom(int x, int y)
	{
		int brokeNum = GameManager.Instance.BossNum;
		if (dices[y, x].Randoms == brokeNum)
		{
			MeshRenderer renderer = dices[y, x].transform.GetChild(0).GetComponent<MeshRenderer>();
			Sequence seq = DOTween.Sequence();
			seq.Append(renderer.material.DOColor(Color.red * 0.8f, 0.3f));
			seq.Append(renderer.material.DOColor(new Color(38, 8, 0) / 255, 0.2f));
			int n = y;
			int m = x;
			seq.AppendCallback(() =>
			{
				StartCoroutine(dices[n, m].BasicDiceNumSelect());
				seq.Kill();
			});
		}
	}
	public void Boom(int x, int y, int value)
	{
		if (x >= GameManager.Instance.Size || x < 0 || y >= GameManager.Instance.Size || y < 0)
			return;
		dices[y, x].DiceNumSelect(value);
		MapNum[y, x] = value;
		GameManager.Instance.BossNum = value;
		Boom(x, y);
	}

	public void Boom(Vector2Int pos, int value)
    {
		if (pos.x >= GameManager.Instance.Size || pos.x < 0 || pos.y >= GameManager.Instance.Size || pos.y < 0)
			return;
		dices[pos.y, pos.x].DiceNumSelect(value);
		MapNum[pos.y, pos.x] = value;
		GameManager.Instance.BossNum = value;
		Boom(pos.x, pos.y);
	}

	public Vector2Int GetRandomNumberPosition(int value)
	{
		List<Vector2Int> diceTrms = new List<Vector2Int>();
		int size = GameManager.Instance.Size;
		int sizeSqr = size * size;
		for (int i = 0; i < sizeSqr; i++)
		{
			int x = i / size;
			int y = i % size;
			if(Instance.MapNum[y, x] == value)
				diceTrms.Add(new Vector2Int(x, y));
		}

		int random = Random.Range(0, diceTrms.Count);

		return diceTrms[random];
	}

	public static int PosToArray(float pos)
	{
		return Mathf.RoundToInt((pos + GameManager.Instance.Offset) / Instance.Distance );
	}

	public static Vector2Int PosToArray(Vector3 pos)
	{
		int x = Mathf.RoundToInt((pos.x + GameManager.Instance.Offset) / Instance.Distance);
		int y = Mathf.RoundToInt((pos.z + GameManager.Instance.Offset) / Instance.Distance);
		return new Vector2Int(x, y);
	}

	public static Vector3 ArrayToPos(int indexX, int indexY)
	{
		float x = indexX * Instance.Distance - GameManager.Instance.Offset;
		float y = indexY * Instance.Distance - GameManager.Instance.Offset;
		return new Vector3(x, 0, y);
	}

	public static Vector3 ArrayToPos(Vector2Int pos)
	{
		return ArrayToPos(pos.x, pos.y);
	}
}
