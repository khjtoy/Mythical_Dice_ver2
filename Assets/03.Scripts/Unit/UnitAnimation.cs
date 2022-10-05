using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitAnimation
{
    [SerializeField]
    private Animator animator;
    public void PlayAnimator(int hash)
    {
        animator.SetTrigger(hash);
    }
}
