using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerDie : CharacterBase
{
    [SerializeField]
    private Image fade;

    private bool isDie;

    private Material spriteMaterial;
    protected override void Start()
    {
        base.Start();
        spriteMaterial = transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().material;
        isDie = false;
    }

    public void DieAction()
    {
        if (!isDie)
        {
            DOTween.KillAll(true);
            isDie = true;
            spriteMaterial.EnableKeyword("_SordColor");
            spriteMaterial.SetFloat("_SordColor", 1f);
            spriteMaterial.DisableKeyword("_SordColor");
            animation.SetBool("IsDie", true);
            animation.SetTrigger("Die");
            fade.DOFade(1, 2f).OnComplete(() =>
            {
                DieScene();
            });
        }
    }

    public void DieScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
