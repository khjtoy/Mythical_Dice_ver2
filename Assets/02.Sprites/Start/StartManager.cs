using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class StartManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _panel;
    [SerializeField]
    private GameObject _exitPanel;

    [SerializeField]
    private GameObject _titlePanel;
    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private RectTransform _fadePanel;

    private Animator _animator;

    private bool _isStart = false;

    private readonly int _startHashStr = Animator.StringToHash("Start");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void StartInGame()
    {
        if(_isStart == false)
        {
            _isStart = true;
            StartCoroutine(StartCoroutine());
        }
        
    }

    private IEnumerator StartCoroutine()
    {
        _exitPanel.SetActive(false);
        _titlePanel.SetActive(false);
        yield return new WaitForSeconds(1f);
        Image image = _panel.GetComponent<Image>();
        Text text = _panel.GetComponentInChildren<Text>();
        text.gameObject.SetActive(false);
        while(true)
        {
            if(image.color.a<=0)
            {
                break;
            }
            image.color = new Color(1, 1, 1, image.color.a - 0.1f);
            yield return new WaitForSeconds(0.05f);
        }

        _animator.SetTrigger(_startHashStr);

        _player.SetActive(true);

        yield return new WaitForSeconds(1.5f);
        Sequence seq = DOTween.Sequence();
        seq.Append(_fadePanel.DOAnchorPos3DY(0, 1f));
        seq.AppendCallback(StartStageScene);
    }

    public void StartStageScene()
    {
        SceneManager.LoadScene("Stage");
    }
    public void ExitGame()
    {
        Application.Quit();
    }

}
