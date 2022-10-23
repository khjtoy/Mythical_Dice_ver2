using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStat : StatBase
{

    [SerializeField]
    private BloodControl bloodCt;
    [SerializeField]
    private HPSlider hpSlider;
    
    [SerializeField] protected int combo = 0;
    public int COMBO { get => combo; }

    private PlayerSkill playerSkill;

    private void Start()
    {
        playerSkill = GetComponent<PlayerSkill>();
    }

    public void GetDamage(int value)
    {
        HP -= value;
        bloodCt.BloodFade(value);
        if (origin_hp * 0.5f >= hp)
            bloodCt.BloodSet(hp, origin_hp);
        playerSkill.Disapper();
        hpSlider.SetHPSlider(HP, origin_hp);
    }
}
