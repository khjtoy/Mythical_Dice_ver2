using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillJump : EnemySkill
{
    enum Sound
    {
        START = 1,
        END,
    }
    Sequence seq = null;
    public override void DoAttack(UnitMove unit, Action ani = null, Action callback = null)
    {
        unit.PlaySound(unit.SkillAudioClips[(int)Sound.START]);
        unit.CanVoid = true;
        seq = DOTween.Sequence();
        if(!unit.Sequence.Sequences.Contains(seq))
            unit.Sequence.Sequences.Add(seq);
        ani?.Invoke();
        Transform baseTrm = unit.transform;
        PlayerMove enemyMove = Define.PlayerMove;
        unit.transform.localScale = (baseTrm.localPosition.x - enemyMove.GamePos.x >= 0) ? new Vector3(1, 1 ,1): new Vector3(-1, 1 ,1);
        seq.Append(baseTrm.DOLocalMoveY(1.3f, 0.5f));
        seq.Join(baseTrm.DOLocalMoveX(Define.PlayerMove.WorldPos.x, 0.3f));
        seq.Join(baseTrm.DOLocalMoveZ(Define.PlayerMove.WorldPos.z, 0.3f));
        seq.Append(baseTrm.DOLocalMoveY(0, 0.2f).SetEase(Ease.InExpo));
        seq.AppendCallback(() =>
        {
            unit.CanVoid = false;
            unit.WorldPos = baseTrm.localPosition;
            Vector2Int pos = unit.GamePos;
            int damage = MapController.Instance.MapNum[pos.y, pos.x];
            unit.StartCoroutine(WaveAttackCoroutine(pos, damage, 0.3f));
            unit.PlaySound(unit.SkillAudioClips[(int)Sound.END]);

            callback?.Invoke();
        });
    }
}
