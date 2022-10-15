using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice:MonoBehaviour
{
	public Dictionary<System.Type, Direct> Directs = new Dictionary<System.Type, Direct>();

	#region ?????
	[SerializeField]
	private BaseSound baseSound;

	private AudioSource audioSource;
	#endregion

	[Header("????? ???? ??")]
	[SerializeField]
	protected int randoms;

	protected Vector3[] DiceRotationVector = new Vector3[]
{ new Vector3(0,0,0), new Vector3(90,0,0), new Vector3(0,0,-90),
	 new Vector3(0,0,90), new Vector3(-90,0,0), new Vector3(180,0,0)};

	protected int Randoms => randoms;

	protected Vector2Int Pos = Vector2Int.zero;

	private enum DiceEffect { Snap}
	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
	}

	public void DiceNumSelect()
	{
		randoms = Random.Range(1, 7);
		transform.localRotation = Quaternion.Euler(DiceRotationVector[randoms - 1]);
		ParticleOn();
		SoundManager.Instance.AudioChange(ref audioSource, baseSound.audioClips[(int)DiceEffect.Snap]);
	}
	public void DiceNumSelect(int value)
	{
		randoms = value;
		transform.localRotation = Quaternion.Euler(DiceRotationVector[randoms - 1]);
		ParticleOn();
		SoundManager.Instance.AudioChange(ref audioSource, baseSound.audioClips[(int)DiceEffect.Snap]);
	}
	public IEnumerator BasicDiceNumSelect(int wait)
	{
		transform.rotation = Quaternion.Euler(0, 0, 0);
		yield return new WaitForSeconds(wait);
		randoms = Random.Range(1, 7);
		MapController.Instance.MapNum[Pos.y, Pos.x] = randoms;
		transform.localRotation = Quaternion.Euler(DiceRotationVector[randoms - 1]);
		ParticleOn();
		SoundManager.Instance.AudioChange(ref audioSource, baseSound.audioClips[(int)DiceEffect.Snap]);
	}
	private void ParticleOn()
	{
		ParticleSystem particleSystem = ObjectPool.Instance.GetObject(PoolObjectType.DiceParticle).GetComponent<ParticleSystem>();
		particleSystem.gameObject.transform.position = this.transform.position + Vector3.up;
		particleSystem.Clear();
		particleSystem.Play();
		StartCoroutine(particleSystem.gameObject.GetComponent<DiceParticleReturn>().ReturnObject());
	}
}
