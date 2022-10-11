using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DiceDirecting : MonoBehaviour
{
	[Header("다이스 할 스피드")]
	[SerializeField]
	private float speed = 5f;

	[Header("다이스 멈추는 시간")]
	[SerializeField]
	private float wait;

	[Header("다이스 현재 값")]
	[SerializeField]
	private int randoms;

	private Vector3[] DiceRotationVector = new Vector3[] 
	{ new Vector3(0,0,0), new Vector3(90,0,0), new Vector3(0,0,-90),
	 new Vector3(0,0,90), new Vector3(-90,0,0), new Vector3(180,0,0)};


	private bool isDiceDirecting = false;

	public int Randoms => randoms;

	public Vector2Int Pos = Vector2Int.zero;

	private Sequence sequence;
	void Update()
	{
		if (isDiceDirecting)
		{
			transform.localRotation = Quaternion.Euler(transform.localEulerAngles + new Vector3(0, 1, 1) * Time.deltaTime * speed);
		}
	}

	public void DiceStart()
	{
		transform.localRotation = Quaternion.Euler(0, 0, 0);
		isDiceDirecting = true;
	}
	public void DiceNumSelect()
	{
		randoms = Random.Range(1, 7);
		transform.localRotation = Quaternion.Euler(DiceRotationVector[randoms-1]);
		isDiceDirecting = false;
		ParticleOn();
	}

	public void DiceNumSelect(int value)
	{
		randoms = value;
		transform.localRotation = Quaternion.Euler(DiceRotationVector[randoms - 1]);
		isDiceDirecting = false;
		ParticleOn();
		//SoundManager.Instance.SetEffectClip((int)EffectEnum.SNAP);
	}
	public IEnumerator BasicDiceNumSelect()
	{
		transform.rotation = Quaternion.Euler(0, 0, 0);
		isDiceDirecting = true;
		yield return new WaitForSeconds(wait);
		randoms = Random.Range(1, 7);
		MapController.Instance.MapNum[Pos.y, Pos.x] = randoms;
		transform.localRotation = Quaternion.Euler(DiceRotationVector[randoms - 1]);
		isDiceDirecting = false;
		ParticleOn();
	}
	public void UpDownSelect()
	{
		isDiceDirecting = true;
		sequence = DOTween.Sequence();
		sequence.Append(this.gameObject.transform.DOMoveY(3f, 0.2f));
		sequence.Append(this.gameObject.transform.DOMoveY(0f, 0.2f));
		sequence.AppendCallback(() => { DiceNumSelect(); });
	}

    public void OnDestroy()
    {
		StopAllCoroutines();
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
