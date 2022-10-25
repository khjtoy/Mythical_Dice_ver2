using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class SoundManager : MonoSingleton<SoundManager>
{
	[SerializeField]
	private AudioMixerGroup audioMixer;

	[FormerlySerializedAs("BackGroundSound")] [SerializeField]
	private SoundSO backGroundSoundSo;

	[SerializeField]
	private AudioSource BackGroundSource;
	public AudioSource effectSource;
	private void Awake()
	{

	}

	private void Start()
	{
		//??? ???????? ??????? ???? ???? ??????? ??
		if (SceneManager.GetActiveScene().name == "SampleScene")
			AudioChange(backGroundSoundSo.audioClips[PlayerPrefs.GetInt("NOWSTAGE")+1], BackGroundSource);
		else if(SceneManager.GetActiveScene().name == "Intro")
			AudioChange(backGroundSoundSo.audioClips[0], BackGroundSource);
		else if(SceneManager.GetActiveScene().name == "Stage")
			AudioChange(backGroundSoundSo.audioClips[1], BackGroundSource);
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
		
	}
}
