using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[System.Serializable]
public class MusicSetting
{
    public float master = 50;
    public float music = 50;
    public float effect = 50;

    public MusicSetting()
    {
        master = 0.5f;
        music = 0.5f;
        effect = 0.5f;
    }

    public MusicSetting(float master, float music, float effect)
    {
        this.master = master;
        this.music = music;
        this.effect = effect;
    }

    public void SetMusicSetting(ref AudioMixer mixer, ref List<Slider> sliders)
    {
        mixer.SetFloat("MASTER",Mathf.Log10(master) * 20);
        mixer.SetFloat("MUSIC",Mathf.Log10(music) * 20);
        mixer.SetFloat("EFFECT", Mathf.Log10(effect) * 20);

        sliders[(int)Sliders.MASTER].value = master;
        sliders[(int)Sliders.MUSIC].value = music;
        sliders[(int)Sliders.EFFECT].value = effect;
    }
}