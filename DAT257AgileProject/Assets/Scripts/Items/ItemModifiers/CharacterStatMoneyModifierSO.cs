using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterStatMoneyModifierSO : CharacterStatModifierSO
{
    public override void AffectCharacter(GameObject character, float money)
    {
        PlayerStatsManager playerStatsManager = FindObjectOfType<PlayerStatsManager>();
        playerStatsManager.Money += (int) money;
    }
}
