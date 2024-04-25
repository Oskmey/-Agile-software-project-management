using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ModifierData
{
    [SerializeField]
    private CharacterStatModifierSO statModifier;
    private float amount;
    public float Amount
    {
        get { return amount; }
        set { this.amount = value; }
    }

    public CharacterStatModifierSO StatModifier { get { return statModifier; } }

}
