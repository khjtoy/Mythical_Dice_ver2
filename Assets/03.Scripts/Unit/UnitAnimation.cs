using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class  UnitAnimation
{
    [SerializeField]
    private Animator _animator;

    public void SetAnimator(Animator animator)
    {
        _animator = animator;
    }
    public void PlayAnimator(int hash)
    {
        _animator.SetTrigger(hash);
    }
}
