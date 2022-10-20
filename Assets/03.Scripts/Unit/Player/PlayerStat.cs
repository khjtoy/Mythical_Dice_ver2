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
    private Text comboText;
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

        // To Do
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            GetDamage(10);
        }
    }

    public void GetDamage(int value)
    {
        HP -= value;
        bloodCt.BloodFade(value);
        if (origin_hp * 0.5f >= hp)
            bloodCt.BloodSet(hp, origin_hp);
        combo = 0;
        SetHPSlider();
        StatUI();
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

    public void SetCombo(int value = 0)
    {
        combo += value;
        if (combo > 20) combo = 20;
        StatUI();
    }

    public void StatUI()
    {
        comboText.text = $"{combo}";
    }
}
