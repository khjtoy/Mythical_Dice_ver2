using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterBase : MonoBehaviour
{
    protected Animator animation;
    protected AudioSource audioSource;

    private void Awake()
    {
        audioSource = transform.GetComponent<AudioSource>();
        animation = transform.GetChild(0).GetChild(0).GetComponent<Animator>();
    }

    public void PlayAnimator(int hash)
    {
        animation.SetTrigger(hash);
    }
}
