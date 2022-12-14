using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SkillSilent : EnemySkill
{
    private Sequence seq = null;
    Vector2Int randomPos = Vector2Int.zero;

    public override void DoAttack(UnitMove unit, Action ani = null, Action callback = null)
    {
        ani?.Invoke();
        seq = DOTween.Sequence();
        unit.PlaySound(unit.SkillAudioClips[0]);

        if (!unit.Sequence.Sequences.Contains(seq))
            unit.Sequence.Sequences.Add(seq);
        Vector2Int pos = unit.GamePos;
        unit.CanVoid = true;
        int brokeNum = MapController.Instance.MapNum[pos.y, pos.x];
        MapController.Instance.BoomSameNum(brokeNum, new Color(0.1f, 0.5f, 0.8f));
        randomPos = MapController.Instance.GetRandomNumberPosition(brokeNum);
        seq.Append(unit.transform.DOLocalMove(MapController.ArrayToPos(randomPos), 0.5f));

        seq.AppendCallback(() =>
        {
            unit.PlaySound(unit.SkillAudioClips[0]);
            unit.WorldPos = unit.transform.position;
            unit.CanVoid = false;
            callback?.Invoke();
            seq.Kill();
        });
    }
}