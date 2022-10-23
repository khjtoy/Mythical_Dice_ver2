using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill : MonoBehaviour
{
    [SerializeField]
    private Transform dice;
    [SerializeField]
    private Sprite[] numbers;
    [SerializeField]
    private Transform targetPos;
    [SerializeField]
    private Image swordImg;
    [SerializeField]
    private Sprite[] swordSkills;

    private Transform[] dices = new Transform[4];
    private Image[] numbersTransform = new Image[4];

    private Animator swordAnimator;
    private int hashSkill = Animator.StringToHash("IsSkill");

    public int currentIdx = 0;

    private int[] numbersIdx = new int[4];

    Sequence[] seq = new Sequence[4];

    private void Start()
    {
        swordAnimator = swordImg.GetComponent<Animator>();
        swordAnimator.enabled = false;
        for (int i = 0; i < 4; i++)
        {
            dices[i] = dice.GetChild(i).transform;
            numbersTransform[i] = dices[i].GetChild(0).GetComponent<Image>();
        }
    }

    public void StackDice(int number)
    {
        if (currentIdx >= 4) return;
        Debug.Log($"numbers {number - 1}");
        numbersTransform[currentIdx].sprite = numbers[number - 1];
        numbersIdx[currentIdx] = number;
        dices[currentIdx].GetComponent<Animator>().Play("Dice");
        Invoke("ShowNumber", 0.4f);
    }

    public void ShowNumber()
    {
        numbersTransform[currentIdx].gameObject.SetActive(true);
        currentIdx++;

        if (currentIdx >= 4)
        {
            //currentIdx = 0;
            //NumberMove();
            bool isEqul = true;
            for(int i = 1; i < 4; i++)
            {
                if (numbersIdx[0] != numbersIdx[i])
                {
                    isEqul = false;
                    break;
                }
            }
            if (isEqul)
                StartCoroutine("NumberMove");
            else Disapper();
        }
    }

    private IEnumerator NumberMove()
    {
        for(int i = 0; i < 4; i++)
        {
            SetSword(i);
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void Disapper()
    {
        for(int i = 0; i < 4; i++)
        {
            DisapperaNumber(i);
        }
    }    

    private void DisapperaNumber(int idx)
    {
        seq[idx].Kill();
        seq[idx] = DOTween.Sequence();
        numbersTransform[idx].color = Color.red;
        seq[idx].Append(numbersTransform[idx].GetComponent<RectTransform>().DOLocalMoveY(numbersTransform[idx].GetComponent<RectTransform>().localPosition.y + 80, 1));
        seq[idx].Join(numbersTransform[idx].GetComponent<RectTransform>().DOScale(0, 1)).OnComplete(() =>
        {
            numbersTransform[idx].gameObject.SetActive(false);
            if (idx == 3)
                ResetSkill();
        });
        seq[idx].AppendCallback(() => seq[idx].Kill());
    }

    private void SetSword(int idx)
    {
        seq[idx].Kill();    
        seq[idx] = DOTween.Sequence();
        numbersTransform[idx].color = Color.blue;
        seq[idx].Append(numbersTransform[idx].GetComponent<RectTransform>().DOLocalMove(targetPos.localPosition - new Vector3(15 * idx, 0, 0), 1f).SetEase(Ease.OutQuart));
        seq[idx].Join(numbersTransform[idx].GetComponent<RectTransform>().DOScale(0, 1)).OnComplete(() =>
        {
            numbersTransform[idx].gameObject.SetActive(false);

            if (idx == 3)
            {
                //swordAnimator.enabled = true;
                //swordAnimator.SetBool(hashSkill, true);
                ResetSkill();
            }
        });
        seq[idx].InsertCallback(0.7f, ()=>swordImg.sprite = swordSkills[idx]);
        seq[idx].AppendCallback(() => seq[idx].Kill());
    }

    private void ResetSkill()
    {
        currentIdx = 0;

        for(int i = 0; i < 4; i++)
        {
            numbersTransform[i].color = Color.white;
            numbersTransform[i].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            numbersTransform[i].GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        }
    }
}
