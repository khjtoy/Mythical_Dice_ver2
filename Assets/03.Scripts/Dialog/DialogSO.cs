using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Dialog")]
public class DialogSO : ScriptableObject
{
    public List<string> sentenceList = new List<string>();
}