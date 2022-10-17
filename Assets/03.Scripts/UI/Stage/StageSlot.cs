using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    }
    private void SetStageSlot()
    {
        string name = gameObject.name;
        name = name.Replace("Stage_", "");
        if(name.Contains("HARD"))
        {
            name = name.Replace("HARD_", "");
            _isHard = true;
        }

        _slotStage = int.Parse(name);

        int currentStage = PlayerPrefs.GetInt("OPEN");
        int clearStage = PlayerPrefs.GetInt("CLEAR");

        _numberImage.sprite = _numberSprite[_slotStage-1];

        if(currentStage >= _slotStage)
        {
            _lockImage.gameObject.SetActive(false);
            if(clearStage - 1 >= _slotStage)
            _stageImage.sprite = _clearSprite;
        }
        else if(clearStage  ==currentStage + 1 && clearStage == _slotStage)
        {
            OpenEvent();
            PlayerPrefs.SetInt("OPEN", clearStage);
        }
    }

    private void OpenEvent()
    {
        Debug.Log("OpenStage: " + _slotStage);
        _lockImage.color = Color.red;
    }
}
