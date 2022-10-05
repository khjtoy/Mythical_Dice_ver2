using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMove : UnitMove
{
	Sequence seq = null;

	float movePos
	{
		get
		{
			return MapController.Instance.Distance;
		}
	}
	private void Start()
	{
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			Translate(Vector3.forward * movePos);
		}
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			Translate(Vector3.back * movePos);
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			Translate(Vector3.left * movePos);
		}
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			Translate(Vector3.right * movePos);
		}

	}
	public override void Translate(Vector3 pos)
	{
		if (_isMoving)
			return;
		_isMoving = true;
		Vector3 original = _pos;
		float offset = 0f;
		if (GameManager.Instance.Size % 2 != 0)
			offset = (GameManager.Instance.Offset - 1) * MapController.Instance.Distance;
		else
			offset = (GameManager.Instance.Offset - 0.5f) * MapController.Instance.Distance;
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
