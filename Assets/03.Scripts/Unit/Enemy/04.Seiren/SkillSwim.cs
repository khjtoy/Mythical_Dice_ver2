using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSwim : EnemySkill
{
    Sequence seq;
    public override void DoAttack(UnitMove unit, Action callback = null)
    {
        seq = DOTween.Sequence();
        seq.Append(unit.transform.DOMoveX(Define.PlayerMove.WorldPos.x, 0.3f));
        seq.Join(unit.transform.DOMoveZ(Define.PlayerMove.WorldPos.z, 0.3f));
        seq.AppendCallback(() =>
        {
            unit.WorldPos = unit.transform.position;
            unit.CanVoid = true;
            unit.StartCoroutine(SwimAttackCoroutine(unit.GamePos, 2, 0.4f, 1));
        });
        seq.AppendInterval(0.8f);
        seq.Append(unit.transform.DOMoveX(Define.PlayerMove.WorldPos.x, 0.3f));
        seq.Join(unit.transform.DOMoveZ(Define.PlayerMove.WorldPos.z, 0.3f));
        seq.AppendCallback(() =>
        {
            unit.WorldPos = unit.transform.position;
            unit.CanVoid = true;
            unit.StartCoroutine(SwimAttackCoroutine(unit.GamePos, 8, 0.4f, 1));
            callback?.Invoke();
        });
    }

}
