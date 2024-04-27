using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu(menuName = "Inventory/Items/EquippableItemSO")]
    public class EquippableItemSO : ItemSO, IDestroyableItem, IItemAction
    {
        [SerializeField]
        private List<ModifierData> modifiersData = new();
        public string ActionName => "Equip";

        [field: SerializeField]
        public string Name { get; private set; }
        //public AudioClip actionSFX { get; private set; }

        public bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
        {
            foreach (ModifierData data in modifiersData)
            {
                data.StatModifier.AffectCharacter(character, data.Amount);
            }
            return true;
        }
    }
}