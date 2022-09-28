using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIHead : MonoBehaviour
{
	[SerializeField] private AIState _currentState;
	private bool _canMoveNext = true;
	private bool _canDoAgain = true;

	protected virtual void Update()
	{
		if (_canDoAgain)
		{
			_currentState.DoAction();
			if (!_currentState.IsLoop)
				_canDoAgain = false;
		}
		foreach (var transition in _currentState.Transitions)
		{
			if (transition.PositiveCondition.Count == 0)
				_canMoveNext = true;
			else
			if (transition.IsPositiveAnd)
			{
				_canMoveNext = true;
				foreach (var conditon in transition.PositiveCondition)
				{
					_canMoveNext &= conditon.Result();
				}
			}
			else
			{
				_canMoveNext = false;
				foreach (var conditon in transition.PositiveCondition)
				{
					_canMoveNext |= conditon.Result();
				}
			}

			if (transition.NegativeCondition.Count == 0)
				_canMoveNext &= true;
			else
			if (transition.IsNegativeAnd)
			{
				_canMoveNext &= true;
				foreach (var conditon in transition.NegativeCondition)
				{
					_canMoveNext &= !conditon.Result();
				}
			}
			else
			{
				_canMoveNext = false;
				foreach (var conditon in transition.NegativeCondition)
				{
					_canMoveNext |= !conditon.Result();
				}
			}

			if (_canMoveNext)
			{
				Debug.Log($"Current State Has Changed To {transition.goalState}");
				_currentState = transition.goalState;
				_canDoAgain = true;
			}
		}
	}
}
