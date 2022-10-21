using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSwim : EnemySkill
{
    Sequence seq;
    int times = 0;
    int range = 2;
    bool isUnderWater = false;
    public override void DoAttack(UnitMove unit, Action ani = null, Action callback = null)
    {
        ani?.Invoke();
        switch(times)
        {
            case 0:
                range = 2;
                isUnderWater = true;
                break;
            case 1:
                range = 8;
                isUnderWater = false;
                break;
        }
        seq = DOTween.Sequence();
        seq.Append(unit.transform.DOMoveX(Define.PlayerMove.WorldPos.x, 0.3f));
        seq.Join(unit.transform.DOMoveZ(Define.PlayerMove.WorldPos.z, 0.3f));
        seq.AppendCallback(() =>
        {
            unit.WorldPos = unit.transform.position;
            unit.CanVoid = isUnderWater;
            unit.StartCoroutine(SwimAttackCoroutine(unit.GamePos, range, 0.4f, 1));
          
        });
        if(isUnderWater)
        {
            seq.AppendInterval(0.8f);
            seq.AppendCallback(() => DoAttack(unit, callback));
        }
        else
        {
            seq.AppendInterval(0.8f);
            seq.AppendCallback(() => callback?.Invoke());
        }
        times = (times + 1) % 2;
    }

}
