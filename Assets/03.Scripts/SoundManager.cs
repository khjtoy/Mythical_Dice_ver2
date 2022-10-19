using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

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
		AudioChange(BackGroundSound.audioClips[0], BackGroundSource);
	}

	public void AudioChange(AudioClip audioClip ,AudioSource audioSource = null)
	{
		audioSource.Stop();
		audioSource.clip = audioClip;
		audioSource.Play();
	}

	public void SetAudioSpeed(ref AudioSource audioSource,float speed)
	{
		audioSource.pitch = speed;
	}
}
