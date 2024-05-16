using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu(menuName = "Inventory/Items/EquippableItemSO")]
    public class EquippableItemSO : ItemSO, IDestroyableItem, IItemAction
    {
        //[SerializeField]
        //private List<ModifierData> modifiersData = new();
        // TODO: equip to accessory from inventory action
        public string ActionName => null;
        
        [SerializeField]
        private AccessorySO accessory;
        public AccessorySO Accessory => accessory;



        //public AudioClip actionSFX { get; private set; }

        public void Awake()
        {
            Name = accessory.AccessoryName;
            //trashModifierData.Amount = trashData.MoneyValue;
        }

        public bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
        {
            // foreach (ModifierData data in modifiersData)
            // {
            //  data.StatModifier.AffectCharacter(character, data.Amount);
            // }
            return true;
        }
    }
}