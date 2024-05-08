using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TrashModifierData
{
    [SerializeField]
    private CharacterStatMoneyModifierSO statModifier;
    private int money;
    public int Money
    {
        get { return money; }
        set { this.money = value; }
    }

    public CharacterStatMoneyModifierSO StatModifier { get { return statModifier; } }
}
