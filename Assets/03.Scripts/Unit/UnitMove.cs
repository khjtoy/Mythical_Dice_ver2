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
            return MapController.PosToArray(_pos); 
        }
    }
    protected bool _isMoving = false;
    public abstract void Translate(Vector3 pos);


    [SerializeField]
    protected UnitAnimation animation;
}
