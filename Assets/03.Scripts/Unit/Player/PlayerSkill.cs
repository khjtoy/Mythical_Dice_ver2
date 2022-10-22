using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill : MonoBehaviour
{
    [SerializeField]
    private Transform dice;
    [SerializeField]
    private Sprite[] numbers;

    private Transform[] dices = new Transform[4];
    private Image[] numbersTransform = new Image[4]; 


    public int currentIdx = 0;

    private void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            dices[i] = dice.GetChild(i).transform;
            numbersTransform[i] = dices[i].GetChild(0).GetComponent<Image>();
        }
    }

    public void StackDice(int number)
    {
        Debug.Log($"numbers {number - 1}");
        numbersTransform[currentIdx].sprite  = numbers[number - 1];
        dices[currentIdx].GetComponent<Animator>().Play("Dice");
        Invoke("ShowNumber", 0.04f);
    }

    public void ShowNumber()
    {
        numbersTransform[currentIdx].gameObject.SetActive(true);
        currentIdx++;

        if (currentIdx >= 4)
        {
            currentIdx = 0;
            ResetNumber();
        }
    }

    private void ResetNumber()
    {
        for(int i = 0; i < 4; i++)
        {
            numbersTransform[i].gameObject.SetActive(false);
        }
    }
}
