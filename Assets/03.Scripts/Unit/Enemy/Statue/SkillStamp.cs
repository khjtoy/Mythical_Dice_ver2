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
        Transform baseTrm = unit.transform;
        EnemyMove enemyMove = Define.EnemyMove;
        seq = DOTween.Sequence();
        seq.Append(baseTrm.DOLocalMoveY(1, 0.5f));
        seq.Append(baseTrm.DOLocalMoveX(Define.PlayerMove.WorldPos.x, 0.3f));
        seq.Join(baseTrm.DOLocalMoveZ(Define.PlayerMove.WorldPos.z, 0.3f));
        seq.Append(baseTrm.DOLocalMoveY(0, 0.2f).SetEase(Ease.InExpo));
        seq.AppendCallback(() =>
        {
            unit.WorldPos = baseTrm.localPosition;
            Vector2Int pos = unit.GamePos;
            int damage = MapController.Instance.MapNum[pos.y, pos.x];
            MapController.Instance.Boom(pos, damage);
            for(int i = 1; i <= GameManager.Instance.Size; i++)
            {
                if((pos + Vector2Int.up * i).y < GameManager.Instance.Size)
                    MapController.Instance.Boom(pos + Vector2Int.up * i, damage);
                if((pos + Vector2Int.down * i).y >= 0)
                    MapController.Instance.Boom(pos + Vector2Int.down * i, damage);
                if((pos + Vector2Int.left * i).x >= 0)
                    MapController.Instance.Boom(pos + Vector2Int.left * i, damage);
                if((pos + Vector2Int.right * i).x < GameManager.Instance.Size)
                    MapController.Instance.Boom(pos + Vector2Int.right * i, damage);
            }
            callback?.Invoke();
            seq.Kill();
        });
    }

}
