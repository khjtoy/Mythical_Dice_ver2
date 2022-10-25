using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;

public class PlayerStat : StatBase
{
    [SerializeField]
    private AudioSource hitSource;
    [FormerlySerializedAs("playerMoveSounds")][SerializeField]
    private SoundSO hitSoundsSo;

    private enum PlayerHitSound { PlayerHit = 0 }

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

    protected override void Start()
    {
        base.Start();
        
        spriteMaterial = transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().material;
        playerSkill = GetComponent<PlayerSkill>();
    }

    public void GetDamage(int value)
    {
        HP -= value;
        bloodCt.BloodFade(value);
        animation.SetTrigger("Hit");
        spriteMaterial.EnableKeyword("_SordColor");
        spriteMaterial.SetFloat("_SordColor", 0f);
        spriteMaterial.DisableKeyword("_SordColor");
        Define.CameraTrans.DOShakePosition(0.3f);
        SoundManager.Instance.AudioChange(hitSoundsSo.audioClips[(int)PlayerHitSound.PlayerHit], hitSource);
        if (HP <= 0)
            if (GetComponent<PlayerDie>() != null)
                GetComponent<PlayerDie>().DieAction();
        if (origin_hp * 0.5f >= hp)
		{
            SoundManager.Instance.SetAudioSpeed(null, 1.5f);
            bloodCt.BloodSet(hp, origin_hp);
		}
        playerSkill.Disapper();
        hpSlider.SetHPSlider(HP, origin_hp);
    }
}
