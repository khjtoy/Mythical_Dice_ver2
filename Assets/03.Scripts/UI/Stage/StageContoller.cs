using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageContoller : MonoBehaviour
{

    [SerializeField]
    private List<GameObject> stageList = new List<GameObject>();

    [SerializeField]
    private RectTransform _fadePanel;
    private int currentStage;
    private int OpenStage;

    private void Awake()
    {
        Time.timeScale = 1;
        //PlayerPrefs.SetInt("CLEAR", 0);
        //PlayerPrefs.SetInt("OPEN", 0);
        OpenStage = PlayerPrefs.GetInt("OPEN");
        currentStage = PlayerPrefs.GetInt("CLEAR");
    }

    private void Start()
    {
        for (int i = 0; i < OpenStage; i++)
        {
            stageList[i].SetActive(false);
        }
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

        Sequence seq = DOTween.Sequence();
        for (int i = 0; i < currentStage; i++)
        {
            if (stageList[i].activeSelf)
            {
                Debug.Log(i);
                int n = i;
                seq.Append(stageList[i].transform.DOShakePosition(1, 10, 10));
                seq.AppendCallback(() => stageList[n].SetActive(false));
            }
        }
        if (OpenStage < currentStage)
        {
            PlayerPrefs.SetInt("OPEN", currentStage);
        }
    }

    public void Stage(int stage)
    {
        PlayerPrefs.SetInt("NOWSTAGE", stage);
        _fadePanel.anchoredPosition = new Vector3(0, -1080, 0);
        Sequence seq = DOTween.Sequence();
        seq.Append(_fadePanel.DOAnchorPosY(0, 1f));
        //seq.AppendCallback(() => SceneManager.LoadScene("GamePlay 6"));
    }
    private void OnDisable()
    {
        DOTween.KillAll();
    }

}
