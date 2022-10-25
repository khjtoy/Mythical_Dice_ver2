using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDash : EnemySkill
{
    enum Sound
    {
        START,
    }
    Sequence seq;
    public override void DoAttack(UnitMove unit, Action ani = null, Action callback = null)
    {
        unit.PlaySound(unit.SkillAudioClips[(int)Sound.START]);

        seq = DOTween.Sequence();
        if (!unit.Sequence.Sequences.Contains(seq))
        {
            unit.Sequence.Sequences.Add(seq);
        }
        ani?.Invoke();
        int damage = MapController.Instance.MapNum[unit.GamePos.y, unit.GamePos.x];
        float range = GameManager.Instance.Size / 2f * MapController.Instance.Distance;
        Vector3 direction = ((Vector2)Define.PlayerMove.GamePos - unit.GamePos).normalized;
        unit.transform.localScale = (direction.x <= 0) ? new Vector3(1, 1 ,1): new Vector3(-1, 1 ,1);
        Vector2Int dir = new Vector2Int(Mathf.CeilToInt(direction.x), Mathf.CeilToInt(direction.y));
        direction = new Vector3(dir.x, 0, dir.y);
        PushAttack(unit.GamePos, dir, damage);
        for(int i = 1; i <= GameManager.Instance.Size; i++)
        {
            Vector3 temp = unit.WorldPos + direction * i * MapController.Instance.Distance;
            if (temp.x <= -range || temp.x >= range || temp.z <= -range || temp.z >= range)
                break;
            seq.Append(unit.transform.DOLocalMove(temp, 0.15f));
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
