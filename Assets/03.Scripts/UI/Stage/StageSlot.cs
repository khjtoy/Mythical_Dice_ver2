using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class StageSlot : MonoBehaviour
{
    [SerializeField]
    private Sprite _clearSprite;
    [SerializeField]
    private Sprite[] _numberSprite;


    private Image _stageImage;
    private Image _numberImage;
    private Image _lockImage;
    private Button _stageBtn;

    private int _slotStage;
    private bool _isHard = false;
    
    private void Awake()
    {
        _stageImage = transform.GetComponent<Image>();
        _numberImage = transform.Find("Number").GetComponent<Image>();
        _lockImage = transform.Find("Lock").GetComponent<Image>();
        _stageBtn = transform.GetComponent<Button>();
        _stageBtn.onClick.AddListener(Stage);
    }

    private void Start()
    {
        SetStageSlot();
    }
    private void Stage()
    {
        Debug.Log("Stage : " + _slotStage);
        StageContoller.Instance.StagePanel.OpenPanel(_slotStage, _isHard);

    }
    private void SetStageSlot()
    {
        string name = gameObject.name; //�̸��� �����´� [Stage_??_1]
        name = name.Replace("Stage_", ""); //�������� Stage_�� ����
        if(name.Contains("HARD")) //�̸��� HARD�� �ִٸ� hard��� üũ �� Hard_ �� ���� 
        {
            name = name.Replace("HARD_", "");
            _isHard = true;
        }

        _slotStage = int.Parse(name); //�������� ��ȣ�� �����´�

        int currentStage = StageContoller.Instance.CurrentUser.currentStage;
        int clearStage = StageContoller.Instance.CurrentUser.clearStage;

        _numberImage.sprite = _numberSprite[_slotStage-1];

        if(_isHard)
        {
            if(clearStage > 4)
            {
                _lockImage.gameObject.SetActive(false);
            }

            return;
        }

        if(currentStage >= _slotStage)
        {
            _lockImage.gameObject.SetActive(false);
            if(clearStage - 1 >= _slotStage)
            _stageImage.sprite = _clearSprite;
        }
        else if(clearStage  == currentStage + 1 && clearStage == _slotStage)
        {
            OpenEvent();
            StageContoller.Instance.ClearStage(clearStage);
        }
    }

    private void OpenEvent()
    {
        Debug.Log ("["+gameObject.name + "] OpenStage: " + _slotStage);
        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(1);
        seq.Append(_lockImage.transform.DOShakePosition(2, 10, 5));
        seq.AppendCallback(() =>
        {
            _lockImage.gameObject.SetActive(false);
        });
    }

    private void OnDisable()
    {
        DOTween.KillAll();
    }
}
