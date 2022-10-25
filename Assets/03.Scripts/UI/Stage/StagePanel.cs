using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StagePanel : MonoBehaviour
{
    [SerializeField]
    private StageSO _stageSo;

    private int _currentStage = 0;
    private bool _isHard = false;

    RectTransform _rect;

    Image _monsterImage;
    Text _explainText;
    Text _stageExplainText;
    Text _stageClearTime;
    Text _stageBossKillText;
    Button _startBtn;
    Button _backBtn;

    public bool IsOpenPanel { get; set; }
    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
        _monsterImage = transform.Find("MonsterImageFrame/MonsterImage").GetComponent<Image>();
        _explainText = transform.Find("ExplainFrame/Text").GetComponent<Text>();
        _stageExplainText = transform.Find("StageExplain/Text").GetComponent<Text>();
        _stageClearTime = transform.Find("StageClearInfo/BossClearTimeText").GetComponent<Text>();
        _stageBossKillText = transform.Find("StageClearInfo/BossKillText").GetComponent<Text>();
        _startBtn = transform.Find("StartBtn").GetComponent<Button>();
        _startBtn.onClick.AddListener(StartBtn);
        _backBtn = transform.Find("BackBtn").GetComponent<Button>();
        _backBtn.onClick.AddListener(BackBtn);
    }
    public void OpenPanel(int id,bool isHard = false)
    {
        if (IsOpenPanel == true)
            return;

        SetPanelInfo(id, isHard);
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
    private void StartBtn()
    {
        StageContoller.Instance.HideBlackPanel(_currentStage, _isHard ? 1:0);
    }
    private void BackBtn()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(_rect.DOAnchorPosX(-1500, 1));
        seq.AppendCallback(() =>
        {
            IsOpenPanel = false;
        });
    }

    public void SetPanelInfo(int id,bool isHead = false)
    {
        _isHard = isHead;
        LoadStageAchieve(id, isHead);
        _currentStage = id--;
        Stage stage = _stageSo.stages[id];
        _monsterImage.sprite = stage.sprite;
        _explainText.text = stage.bossText;
        _stageExplainText.text = stage.storyText;
    }
    private void LoadStageAchieve(int id,bool isHard)
    {
        UserStageVO vo = StageContoller.Instance.LoadUserData(id, isHard);
        if (vo == null) return;

        _stageClearTime.text = "CLEARTIME" +string.Format(vo.clearTime.ToString());
        _stageBossKillText.text ="KILL" + string.Format(vo.clearCount.ToString());

    }
    private void OnDisable()
    {
        DOTween.KillAll();
    }
}
