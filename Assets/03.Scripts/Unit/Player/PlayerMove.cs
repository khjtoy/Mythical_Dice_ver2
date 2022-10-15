using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;

public class PlayerMove : UnitMove
{
	Sequence seq = null;
	int hashMove = Animator.StringToHash("Move");
	int hashCrouch = Animator.StringToHash("Crouch");
	int hashRise = Animator.StringToHash("Rise");

	float movePos
	{
		get
		{
			return MapController.Instance.Distance;
		}
	}
	private void Start()
	{
		float offset = GameManager.Instance.Offset;
		transform.localPosition = new Vector3(-offset, 0, -offset);
		WorldPos = transform.localPosition;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			Translate(Vector3.forward * movePos);
			PlayAnimator(hashRise);
		}
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			Translate(Vector3.back * movePos);
			PlayAnimator(hashCrouch);
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			Translate(Vector3.left * movePos);
<<<<<<< HEAD
			PlayAnimator(hashSlide);
=======
			animation.PlayAnimator(hashMove);
>>>>>>> origin/main
			transform.localScale = new Vector3Int(-1, 1, 1);
		}
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			Translate(Vector3.right * movePos);
<<<<<<< HEAD
			PlayAnimator(hashSlide);
=======
			animation.PlayAnimator(hashMove);
>>>>>>> origin/main
			transform.localScale = Vector3Int.one;
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
		Vector3 original = _pos;
		float offset = GameManager.Instance.Offset;
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
			Debug.Log(GamePos);
			seq.Kill();
		});
	}
}
