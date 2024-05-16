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

    public override void ApplyEffect()
    {
        GameObject recyclingMachine = GameObject.FindGameObjectWithTag("Recycling Manager");
        if (recyclingMachine != null)
        {
            RecyclingManager recyclingManager = recyclingMachine.GetComponent<RecyclingManager>();
            recyclingManager.MoneyMultiplier += moneyMult;
        }
    }

    public override void UnApplyEffect()
    {
        GameObject recyclingMachine = GameObject.FindGameObjectWithTag("Recycling Manager");
        if (recyclingMachine != null)
        {
            RecyclingManager recyclingManager = recyclingMachine.GetComponent<RecyclingManager>();
            recyclingManager.MoneyMultiplier -= moneyMult;
        }
    }
}
