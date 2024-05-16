using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/CharacterStatModifiers/CharacterStatMoneyModifierSO")]
public class CharacterStatMoneyModifierSO : CharacterStatModifierSO
{
    public override void AffectCharacter(GameObject character, float money)
    {
        PlayerStatsManager playerStatsManager = FindObjectOfType<PlayerStatsManager>();
        playerStatsManager.CurrentMoney += (int) money;
    }
}
