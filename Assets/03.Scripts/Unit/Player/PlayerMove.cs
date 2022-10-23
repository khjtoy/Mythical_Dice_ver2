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

	Queue<Vector3> moveDir = new Queue<Vector3>();

	Vector3 vec;
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
		InputMovement();
		PopMove();
		//Dice Boom Debug
		if (Input.GetKeyDown(KeyCode.Space))
		{
			MapController.Instance.Boom(Vector2Int.zero, 1);
		}
	}

	public void InputMovement()
	{
		if (moveDir.Count > 3) return;
		if (Input.GetKeyDown(KeyCode.UpArrow))
			moveDir.Enqueue(Vector3.forward);
		if (Input.GetKeyDown(KeyCode.DownArrow))
			moveDir.Enqueue(Vector3.back);
		if (Input.GetKeyDown(KeyCode.LeftArrow))
			moveDir.Enqueue(Vector3.left);
		if (Input.GetKeyDown(KeyCode.RightArrow))
			moveDir.Enqueue(Vector3.right);
	}

	public void PopMove()
	{
		if (moveDir.Count > 0 && !_isMoving)
		{
			Vector3 dir = moveDir.Dequeue();
			if (dir == Vector3.left) transform.localScale = new Vector3Int(-1, 1, 1);
			if (dir == Vector3.right) transform.localScale = Vector3Int.one;
			ShootAnimation(dir);
			Translate(dir * movePos);
		}
	}

	public void ShootAnimation(Vector3 dir)
	{
		if (dir == Vector3.forward)
			PlayAnimator(hashRise);
		else if (dir == Vector3.back)
			PlayAnimator(hashCrouch);
		else if (dir == Vector3.left || dir == Vector3.right)
			PlayAnimator(hashMove);
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
			//Debug.Log(GamePos);
			seq.Kill();
		});
	}
}
