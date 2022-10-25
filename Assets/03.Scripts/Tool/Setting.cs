using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

enum Buttons
{
    CONTINUE,
    MENU,
    EXIT,
    COUNT
}

enum Sliders
{
    MASTER,
    MUSIC,
    EFFECT,
    COUNT
}
public class Setting : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer = null;
    [SerializeField] private List<Button> buttons = new List<Button>();
    [SerializeField] private List<Slider> sliders = new List<Slider>();

    [SerializeField] private GameInput _input = null;

    private void Awake()
    {
        sliders[(int)Sliders.MASTER].onValueChanged.AddListener(
            (value) =>
            {
                sliders[(int)Sliders.MASTER].value = value;
                MusicSetting temp = DataManager.LoadJsonFile<MusicSetting>(Application.dataPath + "/Save/Settings", "Audio");
                temp.master = sliders[(int)Sliders.MASTER].value;
                _audioMixer.SetFloat("MASTER",Mathf.Log10(temp.master) * 20);
                string str = DataManager.ObjectToJson(temp);
                DataManager.SaveJsonFile(Application.dataPath + "/Save/Settings", "Audio", str);
            }
        );
        sliders[(int)Sliders.MUSIC].onValueChanged.AddListener(
            (value) =>
            {
                sliders[(int)Sliders.MUSIC].value = value;
                MusicSetting temp = DataManager.LoadJsonFile<MusicSetting>(Application.dataPath + "/Save/Settings", "Audio");
                temp.music = sliders[(int)Sliders.MUSIC].value;
                _audioMixer.SetFloat("MUSIC",Mathf.Log10(temp.music) * 20);
                string str = DataManager.ObjectToJson(temp);
                DataManager.SaveJsonFile(Application.dataPath + "/Save/Settings", "Audio", str);
            }
        );
        sliders[(int)Sliders.EFFECT].onValueChanged.AddListener(
            (value) =>
            {
                sliders[(int)Sliders.EFFECT].value = value;
                MusicSetting temp = DataManager.LoadJsonFile<MusicSetting>(Application.dataPath + "/Save/Settings", "Audio");
                temp.effect = sliders[(int)Sliders.EFFECT].value;  
                _audioMixer.SetFloat("EFFECT", Mathf.Log10(temp.effect) * 20);
                string str = DataManager.ObjectToJson(temp);
                DataManager.SaveJsonFile(Application.dataPath + "/Save/Settings", "Audio", str);
            }
        );
        
        buttons[(int)Buttons.CONTINUE].onClick.AddListener(_input.ToggleSetting);
        buttons[(int)Buttons.MENU].onClick.AddListener(() =>
        {
            _input.ToggleSetting();
            if(SceneManager.GetSceneByName("SampleScene").isLoaded)
                GameManager.Instance.LoadStageScene(6);
        });
        buttons[(int)Buttons.EXIT].onClick.AddListener(() =>
        {
            _input.ToggleSetting();
            Application.Quit();
        });
    }

    private void Start()
    {
        DataManager.LoadJsonFile<MusicSetting>(Application.dataPath + "/Save/Settings", "Audio").SetMusicSetting(ref _audioMixer, ref sliders);

    }
}

