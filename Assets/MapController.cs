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


	private Vector2 min;
	private GameObject[][] map;
    public DiceDirecting[][] dices;
	private int[][] mapCost;
	public GameObject[][] MAP { get => map; }

	private GameManager gameManager;

	[SerializeField]
	private float wait;

	public bool isDual;
	public bool XAxis;
	public bool isDown;
	public bool isLeft;
	private bool firstTutorial = false;
	private Vector2 condition;

    protected override void Init()
    {
       //DONDESTORY
    }
    private void Awake()
    {
		gameManager = GameManager.Instance;

		
	}
    private void Start()
    {
		InitMap();
    }
    public void InitMap()
    {
		min = new Vector2(GameManager.Instance.Size / 2, GameManager.Instance.Size / 2) * -1.5f;
		map = new GameObject[gameManager.Height][];
		dices = new DiceDirecting[gameManager.Height][];
		mapCost = new int[gameManager.Height][];
		SpawnMap();
	}

	public int GetIndexCost(int x, int y)
    {
		return mapCost[y][x];
    }


    private void SpawnMap()
    {
        for (int y = 0; y < gameManager.Height; y++)
        {
            map[y] = new GameObject[gameManager.Width];
            dices[y] = new DiceDirecting[gameManager.Width];
			mapCost[y] = new int[gameManager.Width];
            for (int x = 0; x < gameManager.Width; x++)
            {
				mapCost[y][x] = 0;
                map[y][x] = Instantiate(dicePrefabs, new Vector3(0,0,0), Quaternion.identity);
                map[y][x].transform.SetParent(root);
                map[y][x].transform.localPosition = new Vector3(min.x+ (1.5f * x), 0, min.y + (1.5f * y));
                map[y][x].transform.localRotation = Quaternion.Euler(180, 0, 0);
                map[y][x].transform.localScale = new Vector3(1, 1, 1);
				dices[y][x] = map[y][x].transform.GetComponent<DiceDirecting>();
				dices[y][x].Pos = new Vector2Int(x, y);
            }
        }
		FloorDirect();
	}
	private void Update()
	{
		/*
		if (Input.GetMouseButtonDown(0))
			FloorDirect();
	}

	private void SpawnMap()
	{
		for (int y = 0; y < gameManager.Height; y++)
		{
			map[y] = new GameObject[gameManager.Width];
			for (int x = 0; x < gameManager.Width; x++)
			{
				map[y][x] = Instantiate(dicePrefabs, new Vector3(0, 0, 0), Quaternion.identity);
				map[y][x].transform.SetParent(root);
				map[y][x].transform.localPosition = new Vector3(min.x + (1.5f * x), min.y + (1.5f * y), 0);
				map[y][x].transform.localRotation = Quaternion.Euler(180, 0, 0);
				map[y][x].transform.localScale = new Vector3(1, 1, 1);
			}
		}
		*/
	}

	private void FloorDirect(int x = 0, int y = 0, bool isfirst = false)
	{

		if (isDown && !isfirst)
		{
			y = GameManager.Instance.Height - 1;
			condition = new Vector2(condition.x, 0);
		}

		if (!isDown && !isfirst)
			condition = new Vector2(condition.x, GameManager.Instance.Height - 1);

		if (isLeft && !isfirst)
		{
			x = GameManager.Instance.Width - 1;
			condition = new Vector2(0, condition.y);
		}
		if (!isLeft && !isfirst)
			condition = new Vector2(GameManager.Instance.Width - 1, condition.y);

		if ((y < 0 || y >= GameManager.Instance.Height || x < 0 || x >= GameManager.Instance.Width) && isDual)
		{
			return;
		}

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
				if (y >= GameManager.Instance.Height)
				{
					y = GameManager.Instance.Height - 1;
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
				if (x >= GameManager.Instance.Width)
				{
					x = GameManager.Instance.Width - 1;
					y += isDown ? -1 : 1;
					isLeft = true;
				}
			}
		}

		map[y][x].transform.localRotation = Quaternion.Euler(0, 0, 0);
		map[y][x].transform.GetComponent<DiceDirecting>().isDiceDirecting = true;


		isfirst = true;
		StartCoroutine(WaitFloor(x, y, isfirst));
	}

	private IEnumerator WaitFloor(int x, int y, bool isfirst)
	{
		yield return new WaitForSeconds(wait);

		map[y][x].transform.GetComponent<DiceDirecting>().DiceNumSelect();
		mapCost[y][x] = map[y][x].transform.GetComponent<DiceDirecting>().randoms;

		if (!isDual)
		{
			if (x == condition.x && y == condition.y)
			{
				BoomMap.Instance.Boom();
				GameManager.Instance.StageStart = true;
				yield break;
			}

			if (XAxis)
				x += isLeft == true ? -1 : 1;
			else
				y += isDown == true ? -1 : 1;
			FloorDirect(x, y, isfirst);
		}
		else
		{
			if (condition.x == x && condition.y == y)
			{
				GameManager.Instance.StageStart = true;
				yield break;
			}

			if (isLeft)
				FloorDirect(x - 1, y, isfirst);
			else
				FloorDirect(x + 1, y, isfirst);

			if (isDown)
				FloorDirect(x, y - 1, isfirst);
			else
				FloorDirect(x, y + 1, isfirst);
		}
	}

	public static int PosToArray(float pos)
	{
		return Mathf.RoundToInt(pos / 1.5f + GameManager.Instance.Size / 2);
	}

	public static Vector3 ArrayToPos(int indexX, int indexY)
    {
		return new Vector3((GameManager.Instance.Size / 2 * -1.5f) + (1.5f * indexX), (GameManager.Instance.Size / 2 * -1.5f) + (1.5f * indexY), 0);
	}

}
