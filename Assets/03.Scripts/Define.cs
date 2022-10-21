using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Define
{
    private static Transform _cameraTrans;
    private static Transform _enemyTrans;
    private static Transform _playerTrans;
    private static PlayerMove _playerMove;
    private static PlayerStat _playerStat;
    private static EnemyStat _enemyStat;
    private static EnemyMove _enemyMove;

    public static bool IsBossAlive = true;

    public static Transform CameraTrans
    {
        get
        {
            if (_cameraTrans == null)
            {
                _cameraTrans = Camera.main.transform;
            }
            return _cameraTrans;
        }
    }
    public static Transform EnemyTrans
    {
        get
        {
            if (_enemyTrans == null)
            {
                _enemyTrans = GameObject.FindGameObjectWithTag("Boss").transform;
            }
            return _enemyTrans;
        }
    }
    public static Transform PlayerTrans
    {
        get
        {
            if (_playerTrans == null)
            {
                _playerTrans = GameObject.Find("Player").GetComponent<Transform>();
            }
            return _playerTrans;
        }
    }

    public static PlayerMove PlayerMove
    {
        get
        {
            if (_playerMove == null)
            {
                _playerMove = GameObject.Find("Player").GetComponent<PlayerMove>();
            }
            return _playerMove;
        }
    }
    
    public static PlayerStat PlayerStat
    {
        get
        {
            if (_playerStat == null)
            {
                _playerStat = GameObject.Find("Player").GetComponent<PlayerStat>();
            }
            return _playerStat;
        }
    }
    public static EnemyStat EnemyStat
    {
        get
        {
            if (_enemyStat == null)
            {
                _enemyStat = GameObject.FindGameObjectWithTag("Boss").GetComponent<EnemyStat>();
            }
            return _enemyStat;
        }
    }
    public static EnemyMove EnemyMove
    {
        get
        {
            if (_enemyMove == null)
            {
                _enemyMove = EnemyTrans.GetComponent<EnemyMove>();
            }
            return _enemyMove;
        }
    }

}
