using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceParticleReturn : MonoBehaviour
{
	private ParticleSystem particleSystem;
	private void Start()
	{
		particleSystem = GetComponent<ParticleSystem>();
	}

	public IEnumerator ReturnObject()
	{
		yield return new WaitForSeconds(0.2f);
		ObjectPool.Instance.ReturnObject(PoolObjectType.DiceParticle, this.gameObject);
	}
}
