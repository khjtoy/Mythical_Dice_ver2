using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class TimeLineController : MonoBehaviour
{
    private PlayableDirector _director;

    bool _isStop = false;
    private void Awake()
    {

        _director = GetComponent<PlayableDirector>();
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
        _director.Pause();
        Time.timeScale = 0;
    }


    public void StartTimeline()
    {
        _isStop = false;
        Time.timeScale = 1;
        _director.Play();
    }
    public void LoadStartScene()
    {
        SceneManager.LoadScene("Start");
    }
    public void StopTimeLine()
    {
        _director.Pause();
    }
    public void PlayTimeLine()
    {
        _director.Play();
    }
}
