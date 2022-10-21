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
        _subSprites = _mainSprite.GetComponentsInChildren<SpriteRenderer>().ToList().FindAll((x)=>x != _mainSprite);
    }

    void Update()
    {
        foreach (var sprite in _subSprites)
        {
            sprite.sprite = _mainSprite.sprite;
        }
    }
}
