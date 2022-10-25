using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public abstract class UnitMove : CharacterBase
{
    [SerializeField] protected UnitSequence _sequence = new UnitSequence();
    [SerializeField] protected Vector3 _pos;
    [SerializeField] private bool canVoid = false;
    
    public List<AudioClip> SkillAudioClips = new List<AudioClip>();
    
    public bool CanVoid { get => canVoid; set => canVoid = value; }
    public UnitSequence Sequence => _sequence;
    
    public Vector3 WorldPos
    {
        get { return _pos; }
        set { _pos = value; }
    }

    public Vector2Int GamePos
    { 
        get
        {
            return MapController.PosToArray(_pos); 
        }
    }
    protected bool _isMoving = false;
    public abstract void Translate(Vector3 pos);

    public void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource?.Play();
    }
}
