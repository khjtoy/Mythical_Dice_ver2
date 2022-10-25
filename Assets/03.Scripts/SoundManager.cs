using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundManager : MonoSingleton<SoundManager>
{
	[SerializeField]
	private AudioMixerGroup audioMixer;

	[SerializeField]
	private SoundSO BackGroundSound;

	[SerializeField]
	private AudioSource BackGroundSource;
	public AudioSource effectSource;

	private void Start()
	{
		//??? ???????? ??????? ???? ???? ??????? ??
		if (SceneManager.GetSceneByName("SampleScene").isLoaded)
			AudioChange(BackGroundSound.audioClips[PlayerPrefs.GetInt("NOWSTAGE")+1], BackGroundSource);
		else if(SceneManager.GetSceneByName("Intro").isLoaded)
			AudioChange(BackGroundSound.audioClips[0], BackGroundSource);
		else if(SceneManager.GetSceneByName("Stage").isLoaded)
			AudioChange(BackGroundSound.audioClips[1], BackGroundSource);
	}

	public void AudioChange(AudioClip audioClip, AudioSource audioSource = null)
	{
		audioSource.Stop();
		audioSource.clip = audioClip;
		audioSource.Play();
	}

	public void SetAudioSpeed(AudioSource audioSource, float speed)
	{
		if (audioSource == null)
			audioSource = BackGroundSource;

		audioSource.pitch = speed;
	}

	protected override void Init()
	{
		base.Init();
	}
}
