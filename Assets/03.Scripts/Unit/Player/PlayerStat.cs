using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
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

    private PlayerMove playerMove;
    public PlayerMove PlayerMove
    {
        get
        {
            if (playerMove == null)
                playerMove = GetComponent<PlayerMove>();
            return playerMove;
        }
    }

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
