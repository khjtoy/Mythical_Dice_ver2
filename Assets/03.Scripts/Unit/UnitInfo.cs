using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitProperty
{
    HP,
    ATK,
    COMBO,
    COUNT
}

[System.Serializable]
public class UnitInfo
{
    
    [SerializeField] private int origin_hp = 0;
    [SerializeField] private int hp = 0;
    [SerializeField] private int atk = 0;
    [SerializeField] private int combo = 0;

    public int ORG_HP { get => origin_hp; }
    public int HP   { get => hp; }
    public int ATK  { get => atk; }

    public int COMBO { get => combo;  }

    public void SetValue(UnitProperty property, int value)
    {
        switch(property)
        {
            case UnitProperty.HP:
                hp = value;
                break;
            case UnitProperty.ATK:
                atk = value;
                break;
        }
    }

    public void AddValue(UnitProperty property, int value)
    {
        switch (property)
        {
            case UnitProperty.HP:
                hp += value;
                break;
            case UnitProperty.ATK:
                atk += value;
                break;
            case UnitProperty.COMBO:
                combo += value;
                break;
        }
    }
}
