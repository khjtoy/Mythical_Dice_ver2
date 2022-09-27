using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour
{
    [SerializeField] private UnitInfo _info = null;

    public void GetDamage(int value)
    {
        _info.AddValue(UnitProperty.HP, -value);
    }
}
