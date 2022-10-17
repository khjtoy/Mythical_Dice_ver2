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
        _fadePanel.anchoredPosition = new Vector3(0, 0, 0);
        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(1);
        seq.Append(_fadePanel.DOAnchorPos3DY(1080, 1f));
        seq.AppendCallback(() => {
            InitStage();
        });
    }

    private void InitStage()
    {

    }

    private void OnDisable()
    {
        DOTween.KillAll();
    }

}
