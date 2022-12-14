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

	private void Update()
	{
		Check();
	}

	public void AudioChange(AudioClip audioClip, AudioSource audioSource = null)
	{
		audioSource.Stop();
		audioSource.clip = audioClip;
		audioSource.Play();
	}

	//public void SetPitchChange(AudioSource audioSource, float speed)
	//{
		
	//}

	public void SetAudioSpeed(AudioSource audioSource, float speed)
	{
		if (audioSource == null)
			audioSource = BackGroundSource;

		audioSource.pitch = speed;
	}

	public void Check()
	{
		if (SceneManager.GetSceneByName("SampleScene").isLoaded && BackGroundSource.clip != BackGroundSound.audioClips[PlayerPrefs.GetInt("NOWSTAGE") + 2])
		{
			AudioChange(BackGroundSound.audioClips[PlayerPrefs.GetInt("NOWSTAGE") + 2], BackGroundSource);
		}
		else if (SceneManager.GetSceneByName("Intro").isLoaded && BackGroundSource.clip != BackGroundSound.audioClips[0])
			AudioChange(BackGroundSound.audioClips[0], BackGroundSource);
		else if (SceneManager.GetSceneByName("Start").isLoaded && BackGroundSource.clip != BackGroundSound.audioClips[1])
		{
			AudioChange(BackGroundSound.audioClips[1], BackGroundSource);
		}
		else if (SceneManager.GetSceneByName("Stage").isLoaded && BackGroundSource.clip != BackGroundSound.audioClips[2])
		{
			AudioChange(BackGroundSound.audioClips[2], BackGroundSource);
			SetAudioSpeed(BackGroundSource, 1f);
		}
		else if (SceneManager.GetSceneByName("Tutorial").isLoaded && BackGroundSource.clip != BackGroundSound.audioClips[3])
			AudioChange(BackGroundSound.audioClips[3], BackGroundSource);
	}
	protected override void Init()
	{
		base.Init();
	}
}
