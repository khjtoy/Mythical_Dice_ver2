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

        // To Do
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            GetDamage(10);
        }
    }

    public void GetDamage(int value)
    {
        hp -= value;
        combo = 0;
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
            if(hpSliderRt.localScale.x >= whiteSliderRt.localScale.x - 0.01f)
            {
                isDamage = false;
                whiteSliderRt.localScale = hpSliderRt.localScale;
            }
        }
    }

    public int GetCombo(int value)
    {
        if (combo + value <= 20)
            combo++;
        return COMBO;
    }
}
