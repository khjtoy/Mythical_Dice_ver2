using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : StatBase
{
    public void InitStat(int hp)
    {
        origin_hp = hp;
        base.hp = hp;
    }
    public void GetDamage(int value)
    {
        HP -= value;
    }
}
