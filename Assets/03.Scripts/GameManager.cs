using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
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
    public int Offset
    {
        get
        {
            return Mathf.CeilToInt(size / 2f);
        }
    }

    public int Size

    {
        get
        {
            return size;
        }
    }

    private void Start()
    {

        
    }

    protected override void Init()
    {
    }
}
