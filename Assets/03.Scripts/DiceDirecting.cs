using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DiceDirecting : MonoBehaviour
{
	[Header("다이스 할 오브젝트")]
	[SerializeField]
	private GameObject DiceObjet;

	[Header("파티클들")]
	[SerializeField]
	private ParticleSystem[] diceParticel;

	[Header("다이스 할 스피드")]
	[SerializeField]
	private float speed = 5f;

	[Header("다이스 멈추는 시간")]
	[SerializeField]
	private float wait;

	[SerializeField]
	private Vector3[] DiceRotationVector;

	private int randoms;
	private bool isDiceDirecting = false;

	public int Randoms => randoms;

	public Vector2Int Pos = Vector2Int.zero;
	void Update()
	{
		if (isDiceDirecting)
		{
			DiceObjet.transform.localRotation = Quaternion.Euler(DiceObjet.transform.localEulerAngles + new Vector3(0, 1, 1) * Time.deltaTime * speed);
		}
	}

	public void DiceStart()
	{
		isDiceDirecting = true;
	}
	public void DiceNumSelect()
	{
		randoms = Random.Range(1, 7);
		DiceObjet.transform.localRotation = Quaternion.Euler(DiceRotationVector[randoms-1]);
		isDiceDirecting = false;
		for (int i = 0; i < diceParticel.Length; i++)
		{
			diceParticel[i].Clear();
		}
		for (int i = 0; i < diceParticel.Length; i++)
		{
			diceParticel[i].Play();
		}

	}

	public void DiceNumSelect(int value)
	{
		randoms = value;
		DiceObjet.transform.localRotation = Quaternion.Euler(DiceRotationVector[randoms - 1]);
		isDiceDirecting = false;
		for (int i = 0; i < diceParticel.Length; i++)
		{
			diceParticel[i].Clear();
		}
		for (int i = 0; i < diceParticel.Length; i++)
		{
			diceParticel[i].Play();
		}
		//SoundManager.Instance.SetEffectClip((int)EffectEnum.SNAP);
	}
	public IEnumerator BasicDiceNumSelect()
	{
		transform.rotation = Quaternion.Euler(0, 0, 0);
		isDiceDirecting = true;
		Debug.Log("?");
		yield return new WaitForSeconds(wait);
		
		//playerIndex = new Vector2Int(MapController.PosToArray(Define.Player.x), MapController.PosToArray(Define.Player.y));
		//if(playerIndex == Pos)
  //      {
		//	Define.Controller.OnHits(randoms);
  //      }
		randoms = Random.Range(1, 7);
		DiceObjet.transform.localRotation = Quaternion.Euler(DiceRotationVector[randoms - 1]);
		isDiceDirecting = false;
		for (int i = 0; i < diceParticel.Length; i++)
		{
			diceParticel[i].Clear();
		}
		for (int i = 0; i < diceParticel.Length; i++)
		{
			diceParticel[i].Play();
		}
		//if(MapController.PosToArray(this.transform.position.y) == MapController.PosToArray(Define.Player.y))
		//{
		//	Define.Controller.gameObject.GetComponent<OnHit>().OnHits(thisNum);
		//}
	}

    public void OnDestroy()
    {
		StopAllCoroutines();
    }
}
