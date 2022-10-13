using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDash : EnemySkill
{
    Sequence seq;
    public override void DoAttack(UnitMove unit, Action callback = null)
    {
        int damage = MapController.Instance.MapNum[unit.GamePos.y, unit.GamePos.x];
        float range = GameManager.Instance.Size / 2f * MapController.Instance.Distance;
        Vector3 direction = ((Vector2)Define.PlayerMove.GamePos - unit.GamePos).normalized;
        Vector2Int dir = new Vector2Int(Mathf.CeilToInt(direction.x), Mathf.CeilToInt(direction.y));
        direction = new Vector3(dir.x, 0, dir.y);
        seq = DOTween.Sequence();
        PushAttack(unit.GamePos, dir, damage);
        for(int i = 1; i <= GameManager.Instance.Size; i++)
        {
            Vector3 temp = unit.WorldPos + direction * i * MapController.Instance.Distance;
            if (temp.x <= -range || temp.x >= range || temp.z <= -range || temp.z >= range)
                break;
            seq.Append(unit.transform.DOLocalMove(temp, 0.1f));
            seq.AppendCallback(() =>
            {
                unit.WorldPos = unit.transform.localPosition;
                PushAttack(unit.GamePos, dir, damage);
            });
        }
        seq.AppendCallback(() =>
        {
            callback?.Invoke();
        });
    }
}
