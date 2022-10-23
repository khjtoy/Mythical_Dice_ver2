using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPSlider : MonoBehaviour
{
    [Header("HP Slider")]
    [SerializeField]
    private Image playerHPSlider;
    [SerializeField]
    private Image whiteSlider;
    
    public Image iconImage;

    [SerializeField] private float sliderSpeed;

    private bool isDamage = false;

    private RectTransform hpSliderRt;
    private RectTransform whiteSliderRt;

    private void Start()
    {
        hpSliderRt = playerHPSlider.GetComponent<RectTransform>();
        whiteSliderRt = whiteSlider.GetComponent<RectTransform>();
    }

	private void Update()
	{
        UpdateSlider();
    }
	public void SetHPSlider(int hp, int origin_hp)
    {
        isDamage = true;
        //50 47 = 몇 퍼센트냐
        float ratioHp = (100.0f / (float)origin_hp) * (float)hp;
        hpSliderRt.localScale = new Vector3(ratioHp/100, 1, 1);
    }

    private void UpdateSlider()
    {
        if (isDamage)
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
