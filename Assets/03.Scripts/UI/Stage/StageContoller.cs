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

    [SerializeField]
    private User user = null;//자신이 저장하고자 하는 User정보
    public User CurrentUser { get { return user; } }

    public bool OpenPanel { get; }

    private void Awake()
    {
        if(Instance!= null)
        {
            Debug.LogError("Multiple StageController");
        }
        Instance = this;

        user = DataManager.LoadJsonFile<User>(Application.dataPath + "/Save", "SAVEFILE");

        Time.timeScale = 1;
    }

    private void Start()
    {
        
        ShowBlackPanel();
        DOTween.SetTweensCapacity(1000, 1000);
        if (SceneManager.sceneCount < 2)
        {
            SceneManager.LoadScene(5, LoadSceneMode.Additive);
        }
    }
    public UserStageVO LoadUserData(int id, bool isHard)
    {
        UserStageVO vo = null;
        if(isHard)
            user.userHardStages.ForEach(i =>
            {
                if (i.currentStage == id)
                    vo = i;
            });
        else
            user.userStages.ForEach(i =>
            {
                if (i.currentStage == id)
                    vo = i;
            });

        return vo;
    }
    private void ShowBlackPanel()
    {
        _fadePanel.anchoredPosition = new Vector3(0, 0, 0);
        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(1);
        seq.Append(_fadePanel.DOAnchorPos3DY(1080, 1f));
    }
    public void HideBlackPanel(int stage,int isHard = 0)
    {
        _fadePanel.anchoredPosition = new Vector3(0, 0, 0);
        Sequence seq = DOTween.Sequence();
        seq.Append(_fadePanel.DOAnchorPos3DY(-1080, 0f));
        seq.Append(_fadePanel.DOAnchorPos3DY(0, 1f));
        seq.AppendCallback(() =>
        {
            Debug.Log("Stage : " + stage + " HARD :" + isHard);
            PlayerPrefs.SetInt("NOWSTAGE", stage);
            PlayerPrefs.SetInt("HARD", isHard);
            if(stage==1)
            {
                SceneManager.LoadScene("Tutorial2");
            }
            else
            {
                SceneManager.LoadScene("SampleScene");
            }
            
        });
    }
    public void ClearStage(int id)
    {
        user.currentStage = id;
        string str = DataManager.ObjectToJson(user);
        DataManager.SaveJsonFile(Application.dataPath + "/Save", "SAVEFILE", str);
    }

    private void OnDisable()
    {
        DOTween.KillAll();
    }

}
