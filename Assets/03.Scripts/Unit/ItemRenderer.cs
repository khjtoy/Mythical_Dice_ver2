using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRenderer : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer = null;

    private void OnEnable()
    {
        _spriteRenderer.sortingOrder = GameManager.Instance.Size - 1 - MapController.PosToArray(transform.parent.position.z + 0.5f);
    }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
