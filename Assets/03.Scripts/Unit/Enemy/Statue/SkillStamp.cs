using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SkillStamp : EnemySkill
{
    Sequence seq = null;
    public override void DoAttack(Transform baseTrm, Action callback = null)
    {
        EnemyMove enemyMove = Define.EnemyMove;
        seq = DOTween.Sequence();
        seq.Append(baseTrm.DOLocalMoveY(1, 0.5f));
        seq.Append(baseTrm.DOLocalMoveX(Define.PlayerMove.WorldPos.x, 0.3f));
        seq.Join(baseTrm.DOLocalMoveZ(Define.PlayerMove.WorldPos.z, 0.3f));
        seq.Append(baseTrm.DOLocalMoveY(0, 0.2f).SetEase(Ease.InExpo));
        seq.AppendCallback(() =>
        {
            GameManager.Instance.BossNum = MapController.Instance.GetIndexCost(enemyMove.GamePos.x + 1, enemyMove.GamePos.y - 1);
            BoomMap.Instance.Boom(enemyMove.GamePos.x + 1, enemyMove.GamePos.y - 1);
            GameManager.Instance.BossNum = MapController.Instance.GetIndexCost(enemyMove.GamePos.x + 1, enemyMove.GamePos.y + 1);
            BoomMap.Instance.Boom(enemyMove.GamePos.x + 1, enemyMove.GamePos.y + 1);
            GameManager.Instance.BossNum = MapController.Instance.GetIndexCost(enemyMove.GamePos.x - 1, enemyMove.GamePos.y - 1);
            BoomMap.Instance.Boom(enemyMove.GamePos.x - 1, enemyMove.GamePos.y - 1);
            GameManager.Instance.BossNum = MapController.Instance.GetIndexCost(enemyMove.GamePos.x - 1, enemyMove.GamePos.y + 1);
            BoomMap.Instance.Boom(enemyMove.GamePos.x - 1, enemyMove.GamePos.y + 1);
            callback?.Invoke();
        });
    }

}