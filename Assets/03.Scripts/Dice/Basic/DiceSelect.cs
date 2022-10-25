using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DiceSelect : MonoBehaviour
{
	private SoundSO _soundSo;

	private Vector3[] DiceRotationVector = new Vector3[]
{ new Vector3(0,0,0), new Vector3(90,0,0), new Vector3(0,0,-90),
	 new Vector3(0,0,90), new Vector3(-90,0,0), new Vector3(180,0,0)};


	private enum DiceEffect { Snap }

	private int randoms;
	public int Randoms => randoms;
	public Vector2Int pos = Vector2Int.zero;
	public int total = 0;
	private WeightSO weightSO;
	protected virtual void Awake()
	{
		_soundSo = Resources.Load("AudioSO/DiceSoundEffectSO") as SoundSO;
		weightSO = Resources.Load("SO/WeightSO") as WeightSO;

		for (int i = 0; i < weightSO.diceWeights.Count; i++)
		{
			total += weightSO.diceWeights[i].weight;
		}
	}
	public int DiceWeightRandom()
	{
		int weight = 0;
		int selectNum = 0;
		selectNum = Mathf.RoundToInt(total * Random.Range(0.0f, 1.0f));
		for (int i = 0; i < weightSO.diceWeights.Count; i++)
		{
			weight += weightSO.diceWeights[i].weight;
			if (selectNum <= weight)
			{
				return weightSO.diceWeights[i].num;
			}
		}
		return 0;
	}
	public void DiceNumSelect()
	{
		randoms = DiceWeightRandom();
		MapController.Instance.MapNum[pos.y, pos.x] = randoms;
		transform.localRotation = Quaternion.Euler(DiceRotationVector[randoms - 1]);
		ParticleOn();
		SoundManager.Instance.AudioChange(_soundSo.audioClips[(int)DiceEffect.Snap], SoundManager.Instance.effectSource);
	}
	public void DiceNumSelect(int value)
	{
		randoms = value;
		MapController.Instance.MapNum[pos.y, pos.x] = randoms;
		ParticleOn();
		transform.localRotation = Quaternion.Euler(DiceRotationVector[randoms - 1]);
		SoundManager.Instance.AudioChange(_soundSo.audioClips[(int)DiceEffect.Snap], SoundManager.Instance.effectSource);
	}


	/// <summary>
	/// mapcontrol?????? ???? ??
	/// </summary>
	/// <returns></returns>
	public IEnumerator BasicDiceNumSelect(int x, int y, float wait, DiceRotation rotation)
	{
		rotation.Rotation();
		transform.rotation = Quaternion.Euler(0, 0, 0);
		yield return new WaitForSeconds(wait);
		SoundManager.Instance.AudioChange(_soundSo.audioClips[(int)DiceEffect.Snap], SoundManager.Instance.effectSource);
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
