using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatBase : MonoBehaviour
{
    [SerializeField] protected int origin_hp = 0;
    [SerializeField] protected int hp = 0;
    [SerializeField] protected int atk = 0;
    [SerializeField] protected int combo = 0;

    public int ORG_HP { get => origin_hp; }
    public int HP { get => hp; }
    public int ATK { get => atk; }
    public int COMBO { get => combo; }
}
