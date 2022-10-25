using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.Rendering;

public class PlayerMove : UnitMove
{
	[SerializeField]
	private AudioSource moveSource;
	[SerializeField]
	private BaseSound playerMoveSounds;

	private enum PlayerMoveSound { BaseMove = 0}

	Sequence seq = null;
	int hashMove = Animator.StringToHash("Move");
	int hashCrouch = Animator.StringToHash("Crouch");
	int hashRise = Animator.StringToHash("Rise");

	private bool flagAction = false;

	public Queue<Vector3> moveDir = new Queue<Vector3>();

	private PlayerStat playerStat;

	private int playerDir; // 0:Right 1:Left 2:Up 3:Down

	public int PlayerDir => playerDir;

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
		playerStat = GetComponent<PlayerStat>();
		float offset = GameManager.Instance.Offset;
		transform.localPosition = new Vector3(-offset, 0, -offset);
		WorldPos = transform.localPosition;

		EventManager.StartListening("STOPACTION", StopAction);
		EventManager.StartListening("PLAYACTION", PlayAction);
	}

	private void Update()
	{
		if (flagAction) return;
		InputMovement();
		if(!_isMoving || flagAction)
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
			SetDir(dir);

			if (MapController.PosToArray(transform.localPosition + dir) == MapController.PosToArray(Define.EnemyTrans.localPosition)) return;

			if (dir == Vector3.left) transform.localScale = new Vector3Int(-1, 1, 1);
			if (dir == Vector3.right) transform.localScale = Vector3Int.one;
			ShootAnimation(dir);
			Translate(dir * movePos);
		}
	}

	private void SetDir(Vector3 dir)
	{
		if (Input.GetKeyDown(KeyCode.RightArrow))
			playerDir = 0;
		if (Input.GetKeyDown(KeyCode.LeftArrow))
			playerDir = 1;
		if (Input.GetKeyDown(KeyCode.UpArrow))
			playerDir = 2;
		if (Input.GetKeyDown(KeyCode.DownArrow))
			playerDir = 3;
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
		if (_isMoving || flagAction)
			return;
		_isMoving = true;
		SoundManager.Instance.AudioChange(playerMoveSounds.audioClips[(int)PlayerMoveSound.BaseMove], moveSource);
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

    private void StopAction(EventParam eventParam)
    {
		moveDir.Clear();
		seq.Kill();
		seq.timeScale = 0;
		_isMoving = true;
        flagAction = true;
    }

    private void PlayAction(EventParam eventParam)
    {
        flagAction = false;
<<<<<<< HEAD
		_isMoving = false;
=======
        _isMoving = false;
>>>>>>> origin/main
		seq.timeScale = 1;
    }

    private void OnDestroy()
    {
        EventManager.StopListening("STOPACTION", StopAction);
        EventManager.StopListening("PLAYACTION", PlayAction);
    }

    private void OnApplicationQuit()
    {
        EventManager.StopListening("STOPACTION", StopAction);
        EventManager.StopListening("PLAYACTION", PlayAction);
    }
}
