using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMove : UnitMove
{
    Sequence seq = null;
    int hashSlide = Animator.StringToHash("Slide");
    int hashCrouch = Animator.StringToHash("Crouch");
    int hashRise = Animator.StringToHash("Rise");

    private void Update()
    { 
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            Translate(Vector3.forward * 1.5f);
            animation.PlayAnimator(hashRise);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Translate(Vector3.back * 1.5f);
            animation.PlayAnimator(hashCrouch);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Translate(Vector3.left * 1.5f);
            animation.PlayAnimator(hashSlide);
            transform.localScale = new Vector3Int(-1, 1, 1);
            shadow.TurnPosition(true);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Translate(Vector3.right * 1.5f);
            animation.PlayAnimator(hashSlide);
            transform.localScale = Vector3Int.one;
            shadow.TurnPosition(false);
        }
        //Dice Boom Debug
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MapController.Instance.Boom(Vector2Int.zero, 1);
        }
    }
    public override void Translate(Vector3 pos)
    {
        if (_isMoving)
            return;
        _isMoving = true;
        float offset = (GameManager.Instance.Offset - 1) * MapController.Instance.Distance;
        Vector3 original = _pos;
        _pos += pos;
        _pos.x = Mathf.Clamp(_pos.x, -offset, offset);
        _pos.z = Mathf.Clamp(_pos.z, -offset, offset);
        if (Vector3.Distance(original, _pos) == 0f)
        {
            _isMoving = false;
            return;
        }
        seq = DOTween.Sequence();
        seq.Append(transform.DOLocalMove(_pos, 0.17f).SetEase(Ease.InFlash));
        seq.AppendCallback(() =>
        {
            _isMoving = false;
            seq.Kill();
        });
       }
}
