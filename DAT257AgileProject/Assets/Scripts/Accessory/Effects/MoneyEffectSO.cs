using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;

[CreateAssetMenu(fileName = "MoneyEffect", menuName = "Effects/Money Effect")]
public class MoneyEffectSO : EffectSO
{
    [SerializeField]
    private float moneyMult;
    public float MoneyMult => moneyMult;
    private RecyclingManager recyclingManager;
    private void Awake()
    {
        recyclingManager = FindAnyObjectByType<RecyclingManager>();
    }

    public override void ApplyEffect()
    {
        recyclingManager.MoneyMultiplier += moneyMult;
    }

    public override void UnApplyEffect()
    {
        recyclingManager.MoneyMultiplier -= moneyMult;
    }
}