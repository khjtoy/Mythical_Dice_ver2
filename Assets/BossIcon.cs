using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossIcon : MonoBehaviour
{
    [SerializeField]
    private Sprite[] sprites;

    private Image image;

	private void Awake()
	{
		image = GetComponent<Image>();
	}
	private void Start()
	{
		ChangeSprite();
	}
	private void ChangeSprite()
	{
		image .sprite = sprites[PlayerPrefs.GetInt("NOWSTAGE")-1];
	}
}
