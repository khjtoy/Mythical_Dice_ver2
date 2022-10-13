using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SkillSilent : EnemySkill
{
    private Sequence seq = null;
    Vector2Int randomPos = Vector2Int.zero;
    public override void DoAttack(UnitMove unit, Action callback = null)
    {
        seq = DOTween.Sequence();
        Vector2Int pos = unit.GamePos;
        unit.CanVoid = true;
        int brokeNum = MapController.Instance.MapNum[pos.y, pos.x];
        MapController.Instance.BoomSameNum(brokeNum);
        randomPos = MapController.Instance.GetRandomNumberPosition(brokeNum);
        
        seq.Append(unit.transform.DOLocalMove(MapController.ArrayToPos(randomPos), 0.5f));
        
        seq.AppendCallback(() =>
        {
            unit.WorldPos = unit.transform.position;    
            unit.CanVoid = false;
            callback?.Invoke();
        });
    }
}
