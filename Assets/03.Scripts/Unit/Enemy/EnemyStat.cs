using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : StatBase
{
    public void GetDamage(int value)
    {
        HP -= value;
    }
}
