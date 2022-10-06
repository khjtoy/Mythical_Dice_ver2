using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageContoller : MonoBehaviour
{
    [SerializeField]
    StageSO stageSO = null;

    private void Awake()
    {
        
    }

    private void CreateLevelUI()
    {
        for(int i =0;i< stageSO.stages.Count;i++)
        {
            //GameObject levelPanel;
            
            //levelUI.rectTransform.anchoredPosition = stageSO.stages[i].pos;
        }
    }
}
