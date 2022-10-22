using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[System.Serializable]
public class UnitSequence
{
    [SerializeField] private List<Sequence> _sequences = new List<Sequence>();
    public List<Sequence> Sequences => _sequences;

    public void KillAllSequence()
    {
        foreach (var seq in _sequences)
        {
            Debug.Log(seq);
            seq.Kill();
        }
    }
}
