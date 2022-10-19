using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StagePanel : MonoBehaviour
{
    [SerializeField]
    private StageSO _stageSo;

    RectTransform _rect;

    Image _monsterImage;
    Text _explainText;
    Text _stageExplainText;
    Text _stageClearInfoText;
    Button _startBtn;
    Button _backBtn;

    public bool IsOpenPanel { get; set; }
    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
        _monsterImage = transform.Find("MonsterImageFrame/MonsterImage").GetComponent<Image>();
        _explainText = transform.Find("ExplainFrame/Text").GetComponent<Text>();
        _stageExplainText = transform.Find("StageExplain/Text").GetComponent<Text>();
        _stageClearInfoText = transform.Find("StageClearInfo").GetComponent<Text>();
        _startBtn = transform.Find("StartBtn").GetComponent<Button>();
        _backBtn = transform.Find("BackBtn").GetComponent<Button>();
        _backBtn.onClick.AddListener(BackBtn);
    }
    public void OpenPanel(int id)
    {
        if (IsOpenPanel == true)
            return;

        SetPanelInfo(id);
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(() =>
        {
            _rect.anchoredPosition = new Vector2(1500, 0);
        });
        seq.Append(_rect.DOAnchorPosX(0, 1));
        seq.AppendCallback(() =>
        {
            IsOpenPanel = true;
        });
    }
    public void BackBtn()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(_rect.DOAnchorPosX(-1500, 1));
        seq.AppendCallback(() =>
        {
            IsOpenPanel = false;
        });
    }

    public void SetPanelInfo(int id)
    {
        id--;
        Stage stage = _stageSo.stages[id];
        //stage.sprite = _stageSo.stages[id].sprite;
        //stage.bossText = _stageSo.stages[id].bossText;
        //stage.storyText = _stageSo.stages[id].storyText;
        _monsterImage.sprite = stage.sprite;
        _explainText.text = stage.bossText;
        _stageExplainText.text = stage.storyText;
    }
    private void OnDisable()
    {
        DOTween.KillAll();
    }
}
