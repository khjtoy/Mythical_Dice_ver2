using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using DG.Tweening;

public class TimeLineController : MonoBehaviour
{
    private PlayableDirector _playableDirector;

    bool _isStop = false;
    private void Awake()
    {

        _playableDirector = GetComponent<PlayableDirector>();
    }
    private void Start()
    {

    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.X)&&_isStop)
        {
            StartTimeline();
        }
    }

    public void StopTime()
    {
        _isStop = true;
        _playableDirector.Pause();
        Time.timeScale = 0;
        //Sequence seq = DOTween.Sequence();
        //seq.AppendInterval(1);
        //seq.AppendCallback(() => {  });
    }


    public void StartTimeline()
    {
        _isStop = false;
        Time.timeScale = 1;
        _playableDirector.Play();
    }

}
