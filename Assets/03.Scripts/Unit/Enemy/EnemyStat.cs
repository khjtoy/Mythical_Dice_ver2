using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStat : StatBase
{
    [SerializeField]
    private HPSlider hpSlider;
	public void GetDamage(int value)
    {
        HP -= value;
        Debug.Log(HP);
        hpSlider.SetHPSlider(HP, origin_hp);
    }
}
