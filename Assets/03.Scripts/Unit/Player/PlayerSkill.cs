using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerSkill : CharacterBase
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

    public int[] numbersIdx = new int[4];

    bool isCheck = false;

    private CameraZoom cameraZoom;

    private Sprite originSword;

    Sequence[] seq = new Sequence[4];

    private Transform enemy;

    private int damage;
    private void Start()
    {
        originSword = swordImg.sprite;
        enemy = Define.EnemyTrans;
        swordAnimator = swordImg.GetComponent<Animator>();
        cameraZoom = Define.CameraTrans.GetComponent<CameraZoom>();
        swordAnimator.enabled = false;
        for (int i = 0; i < 4; i++)
        {
            dices[i] = dice.GetChild(i).transform;
            numbersTransform[i] = dices[i].GetChild(0).GetComponent<Image>();
        }
    }


    public void StackDice(int number)
    {
        if (currentIdx >= 4 || number == 0) return;
        Debug.Log($"numbers {number - 1}");
        numbersTransform[currentIdx].sprite = numbers[number - 1];
        numbersIdx[currentIdx] = number;
        if (isCheck) return;
        dices[currentIdx].GetComponent<Animator>().Play("Dice");
        dices[currentIdx].DOShakePosition(2f, 0.4f);
        ResetNumber(currentIdx);
        ShowNumber();
    }

    public void ShowNumber()
    {
        numbersTransform[currentIdx].gameObject.SetActive(true);
        currentIdx++;

        if (currentIdx >= 4)
        {
            currentIdx = 0;
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
            {
                damage = numbersIdx[0];
                StartCoroutine("NumberMove");
            }
            else Disapper();
        }
    }

    private IEnumerator NumberMove()
    {
        Define.IsUsingSkill = true;
        EventManager.TriggerEvent("STOPACTION", new EventParam());  
        yield return new WaitForSeconds(0.4f);
        cameraZoom.ZoomTriger = true;
        animation.SetTrigger("Combo");
        for (int i = 0; i < 4; i++)
        {
            SetSword(i);
            yield return new WaitForSeconds(0.65f);
        }

        yield return new WaitForSeconds(0.5f);
        Define.IsUsingSkill = false;
    }

    public void Disapper()
    {
        currentIdx = 0;
        isCheck = true;
        for(int i = 0; i < 4; i++)
        {
            DisapperaNumber(i);
            dices[currentIdx].GetComponent<Animator>().ResetTrigger("Dice");
        }
    }    

    private void DisapperaNumber(int idx)
    {
        dices[idx].GetComponent<Image>().color = new Color(1, 1, 1, 0.6f);
        seq[idx].Kill();
        seq[idx] = DOTween.Sequence();
        numbersTransform[idx].color = Color.red;
        seq[idx].Append(numbersTransform[idx].GetComponent<RectTransform>().DOLocalMoveY(numbersTransform[idx].GetComponent<RectTransform>().localPosition.y + 80, 1));
        seq[idx].Join(numbersTransform[idx].GetComponent<RectTransform>().DOScale(0, 1)).OnComplete(() =>
        {
            numbersTransform[idx].gameObject.SetActive(false);
            dices[idx].GetComponent<Image>().color = new Color(1, 1, 1, 1f);
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
        seq[idx].Append(numbersTransform[idx].GetComponent<RectTransform>().DOLocalMove(targetPos.localPosition + new Vector3(30 * idx, 0, 0), 1f).SetEase(Ease.OutQuart));
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
        seq[idx].InsertCallback(0.15f, () =>
        {
                ComboAttack(0.7f);
        });
        seq[idx].InsertCallback(0.7f, ()=>
        {
            swordImg.sprite = swordSkills[idx];
            if(idx == 3)
            {
                StartCoroutine("Combo");
            }
            
        });
        seq[idx].AppendCallback(() => seq[idx].Kill());
    }

    private void ResetSkill()
    {
        currentIdx = 0;
        isCheck = false;
        for(int i = 0; i < 4; i++)
        {
            numbersIdx[i] = 0;
            numbersTransform[i].color = Color.white;
            numbersTransform[i].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            numbersTransform[i].GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        }
    }

    private IEnumerator Combo()
    {
        yield return new WaitForSeconds(0.1f);
        animation.SetTrigger("Last");
        yield return new WaitForSeconds(0.5f);
        ComboAttack(1, true);
        swordImg.sprite = originSword;
        yield return new WaitForSeconds(0.2f);
        cameraZoom.OutTriger = true;
        EventManager.TriggerEvent("PLAYACTION", new EventParam());
    }

    private void ComboAttack(float f, bool isCombo = false)
    {
        ObjectPool.Instance.GetObject(PoolObjectType.PopUpDamage).GetComponent<NumText>().DamageText(isCombo ? damage * 4 : damage, Define.EnemyStat.transform.position);
        Define.EnemyStat.GetDamage(isCombo ? damage * 4 : damage);
        GameObject particle = ObjectPool.Instance.GetObject(isCombo ? PoolObjectType.ComboParticle : PoolObjectType.AttackParticle);
        particle.transform.position = new Vector3(enemy.localPosition.x, enemy.localPosition.y + 1, enemy.localPosition.z);
        Define.CameraTrans.DOShakePosition(f, 0.2f);
    }

    private void ResetNumber(int idx)
    {
        seq[idx].Kill();
        numbersTransform[currentIdx].gameObject.SetActive(false);
        numbersTransform[idx].color = Color.white;
        numbersTransform[idx].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        numbersTransform[idx].GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
    }
}
