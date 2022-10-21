using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCondition : AICondition
{
    [SerializeField] private EnemyStat _enemyStat = null;
    public override bool Result()
    {
        bool isBossDead = _enemyStat.HP <= 0;

        if(isBossDead)
        {
            Define.PlayerTrans.GetComponent<PlayerAttack>().LastAttack();
        }

        Define.IsBossAlive = !isBossDead;
        return isBossDead;
    }
}
