using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveNumber : MonoBehaviour
{
    [SerializeField]
    private Transform targetPos;

    public Ease ease;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        
    }
    private void OnEnable()
    {
        rectTransform.DOLocalMove(targetPos.localPosition, 1f).SetEase(Ease.OutQuart);
    }

    private void OnDisable()
    {
        rectTransform.localPosition = new Vector3(0, 0, 0);
    }
}
