using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [Header("¸Ê Å©±â ÁöÁ¤")]
    [SerializeField]
    private int width;
    [SerializeField]
    private int height;
    [SerializeField]
    private int size;

    [field: SerializeField]
    public int BossNum { get; set; }

    public int StageNum;

    public bool StageStart;

    public bool thirdTutorial = false;


    [SerializeField]
    private MapController mapController;
    public int Width
    {
        get
        {
            return width;
        }
    }
    public int Height
    {
        get
        {
            return height;
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
