using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;

public class SkillRough : EnemySkill
{
    private Sequence seq;
    Vector2Int movePos = Vector2Int.zero;
    Vector2Int wavePos = Vector2Int.zero;
    private int times = 0;
    public override void DoAttack(UnitMove unit, Action ani = null, Action callback = null)
    {
        Vector2Int pos = unit.GamePos;
        Vector2Int enemyPos = Define.PlayerMove.GamePos;
        seq = DOTween.Sequence();
        int size = GameManager.Instance.Size - 1;
        if (times == 0)
        {
            movePos = new Vector2Int(size, enemyPos.y);
            wavePos = Vector2Int.left;
        }

        if (times == 1)
        {
            movePos = new Vector2Int(enemyPos.x, size);
            wavePos = Vector2Int.down;
        }

        if (times == 2)
        {
            movePos = new Vector2Int(0, enemyPos.y);
            wavePos = Vector2Int.right;
        }

        if (times == 3)
        {
            movePos = new Vector2Int(enemyPos.x, 0);
            wavePos = Vector2Int.up;
        }
        seq.Append(unit.transform.DOLocalMove(MapController.ArrayToPos(movePos), 0.5f));
        seq.AppendCallback(() =>
        {
            unit.WorldPos = unit.transform.localPosition;
            unit.StartCoroutine(LineWaveAttack(wavePos, 6, 0.2f));
        });
        if (times == 3)
        {
            seq.AppendInterval(1);
            seq.AppendCallback(() =>
            {
                times = (times + 1) % 4;
                callback?.Invoke();
            });
        }
        else
        {
            seq.AppendInterval(1);
            seq.AppendCallback(() =>
            {
                times = (times + 1) % 4;
                DoAttack(unit, callback);
            });
        }

    }
}
