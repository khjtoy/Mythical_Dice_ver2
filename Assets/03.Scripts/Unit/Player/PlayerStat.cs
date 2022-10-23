using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStat : StatBase
{
    [SerializeField] private float sliderSpeed;

    [Header("HP Slider")]
    [SerializeField]
    private Image playerHPSlider;
    [SerializeField]
    private Image whiteSlider;
    [SerializeField]
    private BloodControl bloodCt;
    
    [SerializeField] protected int combo = 0;
    public int COMBO { get => combo; }

    private RectTransform hpSliderRt;
    private RectTransform whiteSliderRt;

    private bool isDamage;

    private void Start()
    {
        hpSliderRt = playerHPSlider.GetComponent<RectTransform>();
        whiteSliderRt = whiteSlider.GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (isDamage)
            UpdateSlider();
    }

    public void GetDamage(int value)
    {
        HP -= value;
        bloodCt.BloodFade(value);
        if (origin_hp * 0.5f >= hp)
            bloodCt.BloodSet(hp, origin_hp);
        //combo = 0;
        SetHPSlider();
        isDamage = true;
    }
    private void SetHPSlider()
    {
        float ratioHp = (float)hp / origin_hp;
        hpSliderRt.localScale = new Vector3(ratioHp, 1, 1);
        
    }

    private void UpdateSlider()
    {
        if(isDamage)
        {
            whiteSliderRt.localScale = Vector3.Lerp(whiteSliderRt.localScale, hpSliderRt.localScale, sliderSpeed * Time.deltaTime);

            
            if (hpSliderRt.localScale.x >= whiteSliderRt.localScale.x - 0.01f)
            {
                isDamage = false;
                whiteSliderRt.localScale = hpSliderRt.localScale;
            }
        }
    }
}
