using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class DeathState : EnemySkill
{
    bool _firstDead = true;
    public override void DoAttack(UnitMove unit, Action ani = null, Action callback = null)
    {
        unit.Sequence.KillAllSequence();
        ani?.Invoke();

        if(_firstDead == true)
        {
            _firstDead = false;
            GameManager.Instance.SaveUserData(PlayerPrefs.GetInt("NOWSTAGE") ,GameManager.Instance.Timer );
        }
         
    }
}
