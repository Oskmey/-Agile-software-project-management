using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu(menuName = "Inventory/Items/ConsumableItemSO")]
    public class ConsumableItemSO : ItemSO, IDestroyableItem, IItemAction
    {
        [SerializeField]
        private List<ModifierData> modifiersData = new();
        public string Actioname => "Consume";
        //public AudioClip actionSFX { get; private set; }

        public bool PerformAction(GameObject character)
        {
            foreach (ModifierData data in modifiersData)
            {
                data.statModifier.AffectCharacter(character, data.value);
            }
            return true;
        }
    }

    public interface IDestroyableItem
    {

    }

    public interface IItemAction
    {
        public string Actioname { get;}
        //public AudioClip actionSFX { get; set; }
        bool PerformAction(GameObject character);
    }

    [Serializable]
    public class ModifierData 
    {
        public CharacterStatModifierSO statModifier;
        public float value;
    }
}