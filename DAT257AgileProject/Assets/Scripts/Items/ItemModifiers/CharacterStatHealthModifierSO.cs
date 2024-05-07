using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/CharacterStatModifiers/CharacterStatHealthModifierSO")]
public class CharacterStatHealthModifierSO : CharacterStatModifierSO
{
    public override void AffectCharacter(GameObject character, float value)
    {
        // Health health = character.GetComponent<Health>();
        // if (health != null)
        // health.AddHealth((int)value);
    }
}
