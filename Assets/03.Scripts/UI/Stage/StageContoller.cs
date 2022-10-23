using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageContoller : MonoBehaviour
{
    public static StageContoller Instance;
    [SerializeField]
    private RectTransform _fadePanel;
    [SerializeField]
    private StagePanel _stagePanel;
    public StagePanel StagePanel => _stagePanel;

    public bool OpenPanel { get; }

    private void Awake()
    {
        if(Instance!= null)
        {
            Debug.LogError("Multiple StageController");
        }
        Instance = this;
        Time.timeScale = 1;
        PlayerPrefs.SetInt("CLEAR", 5);
        PlayerPrefs.SetInt("OPEN", 5);
    }

    private void Start()
    {
        ShowBlackPanel();
    }
    private void ShowBlackPanel()
    {
        _fadePanel.anchoredPosition = new Vector3(0, 0, 0);
        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(1);
        seq.Append(_fadePanel.DOAnchorPos3DY(1080, 1f));
    }
    public void HideBlackPanel(int stage)
    {
        _fadePanel.anchoredPosition = new Vector3(0, 0, 0);
        Sequence seq = DOTween.Sequence();
        seq.Append(_fadePanel.DOAnchorPos3DY(-1080, 0f));
        seq.Append(_fadePanel.DOAnchorPos3DY(0, 1f));
        seq.AppendCallback(() =>
        {
            PlayerPrefs.SetInt("NOWSTAGE", stage);
            SceneManager.LoadScene("SampleScene");
        });
    }

    private void OnDisable()
    {
        DOTween.KillAll();
    }

}
