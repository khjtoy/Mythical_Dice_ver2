using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStat : StatBase
{
    [SerializeField]
    private HPSlider hpSlider;
	public void Awake()
	{
        hpSlider=GameObject.Find("BossBar").GetComponent<HPSlider>();
	}
	public void InitStat(int hp)
    {
        origin_hp = hp;
        base.hp = hp;
    }
    public void GetDamage(int value)
    {
        HP -= value;
        hpSlider.SetHPSlider(HP, origin_hp);
    }
}
