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
	void Update()
	{
		if (isDiceDirecting)
		{
			transform.localRotation = Quaternion.Euler(transform.localEulerAngles + new Vector3(0, 1, 1) * Time.deltaTime * speed);
		}
	}

	public void DiceStart()
	{
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
		
		//playerIndex = new Vector2Int(MapController.PosToArray(Define.Player.x), MapController.PosToArray(Define.Player.y));
		//if(playerIndex == Pos)
  //      {
		//	Define.Controller.OnHits(randoms);
  //      }
		randoms = Random.Range(1, 7);

		MapController.Instance.MapNum[Pos.y, Pos.x] = randoms;
		transform.localRotation = Quaternion.Euler(DiceRotationVector[randoms - 1]);
		isDiceDirecting = false;
		ParticleOn();
		//if(MapController.PosToArray(this.transform.position.y) == MapController.PosToArray(Define.Player.y))
		//{
		//	Define.Controller.gameObject.GetComponent<OnHit>().OnHits(thisNum);
		//}
	}

	Sequence sequence;
	public void UpDownSelect()
	{
		Debug.Log("?");
		isDiceDirecting = true;
		sequence.Append(this.gameObject.transform.DOMoveY(2f, 0.5f));
		sequence.Append(this.gameObject.transform.DOMoveY(0f, 0.5f));
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
