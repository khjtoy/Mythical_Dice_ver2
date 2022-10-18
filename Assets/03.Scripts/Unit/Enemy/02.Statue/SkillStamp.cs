using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SkillStamp : EnemySkill
{
    Sequence seq = null;

    public override void DoAttack(UnitMove unit, Action callback = null)
    {
        int skillCase = UnityEngine.Random.Range(0, 2);
        Transform baseTrm = unit.transform;
        EnemyMove enemyMove = Define.EnemyMove;
        unit.CanVoid = true;
        seq = DOTween.Sequence();
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
            switch (skillCase)
            {
                case 0:
                    {
                        CrossAttack(pos, damage);
                        break;
                    }
                case 1:
                {
                        SquareRangeAttack(pos, 1, damage);
                        break;
                    }
            }

            
            callback?.Invoke();
            seq.Kill();
        });
    }

}
