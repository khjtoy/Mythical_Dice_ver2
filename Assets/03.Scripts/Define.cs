using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Define
{
    private static Transform _playerTrans;
    public static Transform PlayerTrans
    {
        get
        {
            if( _playerTrans == null )
            {
                _playerTrans = GameObject.Find("Player").GetComponent<Transform>();
            }
            return _playerTrans;
        }
    }
}
