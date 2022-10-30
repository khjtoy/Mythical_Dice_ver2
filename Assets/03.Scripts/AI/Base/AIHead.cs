using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIHead : MonoBehaviour
{
	[SerializeField] private AIState _currentState;
	private AIState anyState = null;
	private bool _canMoveNext = true;
	private bool _canDoAgain = true;
	private bool _isExcuting = false;

	public void Init(AIState state)
	{
		_currentState = state;
		anyState = transform.Find("State/ANY").GetComponent<AIState>();
	}

	protected virtual void Update()
	{
		if (Define.IsMapLoaded == false)
			return;
		SetState(anyState);
		if (_canDoAgain)
		{
			_isExcuting = true;
			_currentState.DoAction(EndState);
			if (!_currentState.IsLoop)
				_canDoAgain = false;
		}
        if (_isExcuting == false)
        {
			SetState(_currentState);
		}
	}

	void SetState(AIState state)
	{
		foreach (var transition in state.Transitions)
		{
			if (transition.PositiveCondition.Count == 0)
				_canMoveNext = true;
			else if (transition.IsPositiveAnd)
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
			else if (transition.IsNegativeAnd)
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
	private void EndState()
    {
		_isExcuting = false;
    }
}
