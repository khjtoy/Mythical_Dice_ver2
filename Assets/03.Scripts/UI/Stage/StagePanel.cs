using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StagePanel : MonoBehaviour
{
    RectTransform _rect;

    Image _monsterImage;
    Text _explainText;
    Text _stageExplainText;
    Text _stageClearInfoText;
    Button _startBtn;
    Button _backBtn;
    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
        _monsterImage = transform.Find("MonsterImageFrame/MonsterImage").GetComponent<Image>();
        _explainText = transform.Find("ExplainFrame/Text").GetComponent<Text>();
        _stageExplainText = transform.Find("MonsterImageFrame/MonsterImage").GetComponent<Text>();
        _stageClearInfoText = transform.Find("MonsterImageFrame/MonsterImage").GetComponent<Text>();
        _startBtn = transform.Find("StartBtn").GetComponent<Button>();
        _backBtn = transform.Find("BackBtn").GetComponent<Button>();
    }


}
