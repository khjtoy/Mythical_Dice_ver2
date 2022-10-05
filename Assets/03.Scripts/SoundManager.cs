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

	private AudioSource BackGroundSource;

	private void Awake()
	{
		BackGroundSource = GetComponent<AudioSource>();
	}

	private void Start()
	{
		//�̰� �������� �����Ҷ� ���� ���� �����ָ� ��
		AudioChange(ref BackGroundSource, BackGroundSound.audioClips[0]);
	}

	public void AudioChange(ref AudioSource audioSource, AudioClip audioClip)
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
