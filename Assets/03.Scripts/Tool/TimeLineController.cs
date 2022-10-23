using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimeLineController : MonoBehaviour
{
    private PlayableDirector _playableDirector;
    private void Awake()
    {

        _playableDirector = GetComponent<PlayableDirector>();
    }
    private void Start()
    {

    }

    public void StopTime()
    {
        Time.timeScale = 0;
        _playableDirector.Stop();
    }

    public void StartTimeline()
    {
        Time.timeScale = 1;
        _playableDirector.Play();
    }

}
