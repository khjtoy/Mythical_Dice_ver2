using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCondition : AICondition
{
    [SerializeField] private EnemyStat _enemyStat = null;
    public EnemyStat EnemyStat
    {
        get => _enemyStat;
        set => _enemyStat = value;
    }
    public override bool Result()
    {
        bool isBossDead = _enemyStat.HP <= 0;
        Define.IsBossAlive = !isBossDead;
        return isBossDead;
    }

    public override void SetParam(float param)
    {
        
    }
}
