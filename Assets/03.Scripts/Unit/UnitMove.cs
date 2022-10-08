using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitMove : MonoBehaviour
{
    [SerializeField] protected Vector3 _pos;
    [SerializeField] private bool canVoid = false;
    public bool CanVoid { get => canVoid; set => canVoid = value; }
    public Vector3 WorldPos
    {
        get { return _pos; }
        set { _pos = value; }
    }

    public Vector2Int GamePos
    { 
        get
        {
            int offsetX = Mathf.RoundToInt((_pos.x + GameManager.Instance.Offset) / MapController.Instance.Distance);
            int offsetZ = Mathf.RoundToInt((_pos.z + GameManager.Instance.Offset) / MapController.Instance.Distance);
            Vector2Int pos = new Vector2Int(offsetX, offsetZ);
            return pos; 
        }
    }
    protected bool _isMoving = false;
    public abstract void Translate(Vector3 pos);


    [SerializeField]
    protected UnitAnimation animation;
}
