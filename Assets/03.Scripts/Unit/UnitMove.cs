using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitMove : MonoBehaviour
{
    [SerializeField] protected Vector3 _pos;

    public Vector3 WorldPos
    {
        get { return _pos; }
    }
    public Vector3Int GamePos
    { 
        get
        {
            int offsetX = Mathf.RoundToInt((_pos.x + GameManager.Instance.Offset) / MapController.Instance.Distance);
            int offsetZ = Mathf.RoundToInt((_pos.z + GameManager.Instance.Offset) / MapController.Instance.Distance);
            Vector3Int pos = new Vector3Int(offsetX, 0, offsetZ);
            return pos; 
        }
    }
    protected bool _isMoving = false;
    public abstract void Translate(Vector3 pos);
}
