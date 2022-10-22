using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSlime : EnemySkill
{
    Sequence seq = null;
    public override void DoAttack(UnitMove unit, Action ani = null, Action callback = null)
    {
        ani?.Invoke();
        Transform baseTrm = unit.transform;
        EnemyMove enemyMove = Define.EnemyMove;
        unit.CanVoid = true;
        seq = DOTween.Sequence();
        if(!unit.Sequence.Sequences.Contains(seq))
            unit.Sequence.Sequences.Add(seq);
        seq.Append(baseTrm.DOLocalMoveY(1, 0.5f));
        seq.Append(baseTrm.DOLocalMoveX(Define.PlayerMove.WorldPos.x, 0.3f));
        seq.Join(baseTrm.DOLocalMoveZ(Define.PlayerMove.WorldPos.z, 0.3f));
        seq.Append(baseTrm.DOLocalMoveY(0, 0.2f).SetEase(Ease.InExpo));
        seq.AppendCallback(() =>
        {
            unit.CanVoid = false;
            unit.WorldPos = baseTrm.localPosition;
            Vector2Int pos = unit.GamePos;
            int damage = MapController.Instance.MapNum[pos.y, pos.x];
            SquareRangeAttack(unit.GamePos, 1, MapController.Instance.MapNum[pos.y, pos.x]);

            callback?.Invoke();
            seq.Kill();
        });
    }
}
