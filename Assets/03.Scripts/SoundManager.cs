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
	private BaseSound BackGroundSound;

	[SerializeField]
	private AudioSource BackGroundSource;
	public AudioSource effectSource;
	private void Awake()
	{

	}

	private void Start()
	{
		//이걸 스테이지 시작할때 마다 실행 시켜주면 됨
		if (SceneManager.GetActiveScene().name == "SampleScene")
			AudioChange(BackGroundSound.audioClips[PlayerPrefs.GetInt("NOWSTAGE")+1], BackGroundSource);
		else if(SceneManager.GetActiveScene().name == "Intro")
			AudioChange(BackGroundSound.audioClips[0], BackGroundSource);
		else if(SceneManager.GetActiveScene().name == "Stage")
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
}
