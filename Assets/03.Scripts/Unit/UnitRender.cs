using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitRender : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _mainSprite;
    private List<SpriteRenderer> _subSprites = new List<SpriteRenderer>();

    private void Start()
    {
        _mainSprite = transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
        _subSprites = _mainSprite.GetComponentsInChildren<SpriteRenderer>().ToList().FindAll((x)=>x != _mainSprite);
    }

    void Update()
    {
        foreach (var sprite in _subSprites)
        {
            sprite.sprite = _mainSprite.sprite;
        }
    }

    public void SetSortingLayer()
    {
        _mainSprite.sortingOrder = GameManager.Instance.Size - 1 - MapController.PosToArray(transform.position.z) + GameManager.Instance.Size;

    }
}
