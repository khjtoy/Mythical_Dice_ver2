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
        unit.PlaySound(unit.SkillAudioClips[0]);
        switch(times)
        {
            case 0:
                ani?.Invoke();
                range = 2;
                isUnderWater = true;
                break;
            case 1:
                range = 8;
                isUnderWater = false;
                unit.CanVoid = false;
                break;
        }

        seq = DOTween.Sequence();
        if(!unit.Sequence.Sequences.Contains(seq))
            unit.Sequence.Sequences.Add(seq);
        seq.Append(unit.transform.DOMoveX(Define.PlayerMove.WorldPos.x, 0.3f));
        seq.Join(unit.transform.DOMoveZ(Define.PlayerMove.WorldPos.z, 0.3f));
        seq.AppendCallback(() =>
        {
            unit.WorldPos = unit.transform.localPosition;
            Vector2Int pos = unit.GamePos;
            int damage = MapController.Instance.MapNum[pos.y, pos.x];
            unit.WorldPos = unit.transform.position;
            unit.StartCoroutine(SwimAttackCoroutine(unit.GamePos, range, 0.4f, damage));
          
        });
        if(isUnderWater)
        {
            seq.AppendInterval(0.8f);
            seq.AppendCallback(() =>
            {
                unit.CanVoid = true;
                DoAttack(unit, ani, callback);
            });
        }
        else
        {
            seq.AppendInterval(0.8f);
            seq.AppendCallback(() => callback?.Invoke());
        }
        times = (times + 1) % 2;
    }

}
