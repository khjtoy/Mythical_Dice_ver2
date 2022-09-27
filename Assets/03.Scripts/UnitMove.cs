using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitMove : MonoBehaviour
{
    [SerializeField] protected Vector3 _pos;

    protected Vector3 GamePos
    { 
        get
        {
            Vector3 pos = (_pos + new Vector3(GameManager.Instance.Offset, 0, GameManager.Instance.Offset)) / MapController.Instance.Distance;
            return pos; 
        }
    }
    protected bool _isMoving = false;
    public abstract void Translate(Vector3 pos);
}
