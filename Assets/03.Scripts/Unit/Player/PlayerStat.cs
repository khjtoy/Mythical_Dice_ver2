using DG.Tweening;
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

    private Material spriteMaterial;

    private void Start()
    {
        spriteMaterial = transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().material;
        playerSkill = GetComponent<PlayerSkill>();
    }

    public void GetDamage(int value)
    {
        HP -= value;
        bloodCt.BloodFade(value);
        Vector3 pos = transform.GetChild(0).GetChild(0).localPosition;
        pos.y += 0.8f;
         transform.GetChild(0).GetChild(0).localPosition = pos;
        animation.SetTrigger("Hit");
        spriteMaterial.EnableKeyword("_SordColor");
        spriteMaterial.SetFloat("_SordColor", 0f);
        spriteMaterial.DisableKeyword("_SordColor");
        Define.CameraTrans.DOShakePosition(0.3f);
        if (HP <= 0) 
            GetComponent<PlayerDie>().DieAction();
        if (origin_hp * 0.5f >= hp)
            bloodCt.BloodSet(hp, origin_hp);
        playerSkill.Disapper();
        hpSlider.SetHPSlider(HP, origin_hp);
    }
}
