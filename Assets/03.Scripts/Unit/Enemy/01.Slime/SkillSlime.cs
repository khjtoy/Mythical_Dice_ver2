using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SkillSlime : EnemySkill
{
    Sequence seq = null;

    public override void DoAttack(UnitMove unit, Action ani = null, Action callback = null)
    {
        ani?.Invoke();
        Transform baseTrm = unit.transform;
        EnemyMove enemyMove = Define.EnemyMove;
        unit.CanVoid = true; 
        unit.PlaySound(unit.SkillAudioClips[0]);
        seq = DOTween.Sequence();
        if (!unit.Sequence.Sequences.Contains(seq))
            unit.Sequence.Sequences.Add(seq);
        seq.Append(baseTrm.DOLocalMoveY(4, 0.3f));
        seq.Join(baseTrm.DOLocalMoveX(Define.PlayerMove.WorldPos.x, 0.3f));
        seq.Join(baseTrm.DOLocalMoveZ(Define.PlayerMove.WorldPos.z, 0.3f));
        seq.Insert(0.1f, baseTrm.DOLocalMoveY(0, 0.3f));
        seq.AppendCallback(() =>
        {
            unit.CanVoid = false;
            unit.WorldPos = baseTrm.localPosition;
            Vector2Int pos = unit.GamePos;
            int damage = MapController.Instance.MapNum[pos.y, pos.x];
            SquareRangeAttack(unit.GamePos, 1, MapController.Instance.MapNum[pos.y, pos.x]);
            
            int random = Random.Range(0, 5);

            if(random == 0)
            {
                Define.PlayerAttack.SpawnItem(unit.WorldPos);
            }

            callback?.Invoke();
            seq.Kill();
        });
    }
}
