using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice:MonoBehaviour
{
	public Dictionary<System.Type, Direct> directs = new Dictionary<System.Type, Direct>();
	public Dictionary<System.Type, DiceRotation> rotation = new Dictionary<System.Type, DiceRotation>();

	protected Dice dice;
	#region 
	private BaseSound baseSound;
	#endregion

	protected Vector3[] DiceRotationVector = new Vector3[]
{ new Vector3(0,0,0), new Vector3(90,0,0), new Vector3(0,0,-90),
	 new Vector3(0,0,90), new Vector3(-90,0,0), new Vector3(180,0,0)};

	private int randoms;
	public int Randoms => randoms;

	[HideInInspector]
	public Vector2Int Pos = Vector2Int.zero;

	private enum DiceEffect { Snap}

	protected virtual void Awake()
	{
		dice = GetComponent<Dice>();
		baseSound = Resources.Load("AudioSO/DiceSoundEffectSO") as BaseSound;
	}
	public void DiceNumSelect()
	{
		randoms = Random.Range(1, 7);
		transform.localRotation = Quaternion.Euler(DiceRotationVector[randoms - 1]);
		ParticleOn();
		SoundManager.Instance.AudioChange(ref SoundManager.Instance.effectSource, baseSound.audioClips[(int)DiceEffect.Snap]);
	}
	public void DiceNumSelect(int value)
	{
		randoms = value;
		transform.localRotation = Quaternion.Euler(DiceRotationVector[randoms - 1]);
		ParticleOn();
		SoundManager.Instance.AudioChange(ref SoundManager.Instance.effectSource, baseSound.audioClips[(int)DiceEffect.Snap]);
	}
	public IEnumerator BasicDiceNumSelect(float wait, DiceRotation rotation)
	{
		rotation.Rotation();
		transform.rotation = Quaternion.Euler(0, 0, 0);
		yield return new WaitForSeconds(wait);
		SoundManager.Instance.AudioChange(ref SoundManager.Instance.effectSource, baseSound.audioClips[(int)DiceEffect.Snap]);
		randoms = Random.Range(1, 7);
		MapController.Instance.MapNum[Pos.y, Pos.x] = randoms;
		transform.localRotation = Quaternion.Euler(DiceRotationVector[randoms - 1]);
		rotation.Rotation();
		ParticleOn();
	}

	public IEnumerator BasicDiceNumSelect(int x, int y, float wait, DiceRotation rotation)
	{
		rotation.Rotation();
		transform.rotation = Quaternion.Euler(0, 0, 0);
		Debug.Log("?????>");
		yield return new WaitForSeconds(wait);
		SoundManager.Instance.AudioChange(ref SoundManager.Instance.effectSource, baseSound.audioClips[(int)DiceEffect.Snap]);
		MapController.Instance.WaitFloor(x, y, true);
		rotation.Rotation();
		randoms = Random.Range(1, 7);
		MapController.Instance.MapNum[Pos.y, Pos.x] = randoms;
		transform.localRotation = Quaternion.Euler(DiceRotationVector[randoms - 1]);
		ParticleOn();
	}

	public void Direct(Direct direct, DiceRotation diceRotation)
	{
		direct.Direction(diceRotation);
	}
	private void ParticleOn()
	{
		ParticleSystem particleSystem = ObjectPool.Instance.GetObject(PoolObjectType.DiceParticle).GetComponent<ParticleSystem>();
		particleSystem.gameObject.transform.position = this.transform.position + Vector3.up/2;
		particleSystem.Clear();
		particleSystem.Play();
		StartCoroutine(particleSystem.gameObject.GetComponent<DiceParticleReturn>().ReturnObject());
	}
}
