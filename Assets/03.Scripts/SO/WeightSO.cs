using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct DiceWeight
{
	public int num;
	public int weight;
	public DiceWeight(int num, int weight)
	{
		this.num = num;
		this.weight = weight;
	}
}
[CreateAssetMenu(menuName = "SO/Weight")]
public class WeightSO : ScriptableObject
{
	public List<DiceWeight> diceWeights;
}
