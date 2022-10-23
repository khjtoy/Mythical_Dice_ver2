using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSelect : MonoBehaviour
{
	private BaseSound baseSound;

	private Vector3[] DiceRotationVector = new Vector3[]
{ new Vector3(0,0,0), new Vector3(90,0,0), new Vector3(0,0,-90),
	 new Vector3(0,0,90), new Vector3(-90,0,0), new Vector3(180,0,0)};
	private enum DiceEffect { Snap }

	private int randoms;
	public int Randoms => randoms;
	public Vector2Int pos = Vector2Int.zero;

	protected virtual void Awake()
	{
		baseSound = Resources.Load("AudioSO/DiceSoundEffectSO") as BaseSound;
	}

	public void DiceNumSelect()
	{
		randoms = Random.Range(1, 7);
		MapController.Instance.MapNum[pos.y, pos.x] = randoms;
		transform.localRotation = Quaternion.Euler(DiceRotationVector[randoms - 1]);
		ParticleOn();
		SoundManager.Instance.AudioChange(baseSound.audioClips[(int)DiceEffect.Snap], SoundManager.Instance.effectSource);
	}
	public void DiceNumSelect(int value)
	{
		randoms = value;
		MapController.Instance.MapNum[pos.y, pos.x] = randoms;
		ParticleOn();
		transform.localRotation = Quaternion.Euler(DiceRotationVector[randoms - 1]);
		SoundManager.Instance.AudioChange(baseSound.audioClips[(int)DiceEffect.Snap], SoundManager.Instance.effectSource);
	}


	/// <summary>
	/// mapcontrol에서만 쓰는 애
	/// </summary>
	/// <returns></returns>
	public IEnumerator BasicDiceNumSelect(int x, int y, float wait, DiceRotation rotation)
	{
		rotation.Rotation();
		transform.rotation = Quaternion.Euler(0, 0, 0);
		yield return new WaitForSeconds(wait);
		SoundManager.Instance.AudioChange(baseSound.audioClips[(int)DiceEffect.Snap], SoundManager.Instance.effectSource);
		MapController.Instance.WaitFloor(x, y, true);
		rotation.Rotation();
		DiceNumSelect();
		ParticleOn();
	}

	private void ParticleOn()
	{
		ParticleSystem particleSystem = ObjectPool.Instance.GetObject(PoolObjectType.DiceParticle).GetComponent<ParticleSystem>();
		particleSystem.gameObject.transform.position = this.transform.position + Vector3.up / 2;
		particleSystem.Clear();
		particleSystem.Play();
		StartCoroutine(particleSystem.gameObject.GetComponent<DiceParticleReturn>().ReturnObject());
	}
}
