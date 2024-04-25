using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu(menuName = "Inventory/Items/TrashItemSO")]
    public class TrashItemSO : ItemSO, IDestroyableItem, IItemAction
    {
        //[SerializeField]
        //private ModifierData trashModifierData = new();

        [SerializeField]
        private TrashData trashData;

        public TrashData TrashData
        {
            get { return trashData; }
        }

        // needs trash script from respective trash prefab
        [SerializeField]
        private TrashScript trashScript;

        public TrashType TrashType
        {
            get { return trashScript.TrashType; }
        }

        public TrashRarity TrashRarity
        {
            get { return trashScript.Rarity; }
        }

        public string ActionName => "Temp";
        //public AudioClip actionSFX { get; private set; }

        public void Awake()
        {
            //trashModifierData.Amount = trashData.MoneyValue;
        }

        public bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
        {
            //trashModifierData.statModifier.AffectCharacter(character, trashModifierData.Amount);
            //foreach (ModifierData data in modifiersData)
            //{
               // data.statModifier.AffectCharacter(character, data.Amount);
            //}
            return true;
        }
    }

    public class TrashModifierData
    {
        public CharacterStatMoneyModifierSO statModifier;
        private int money;
        public int Money
        {
            get { return money; }
            set { this.money = value; }
        }

    }

    [Serializable]
    public class ModifierData
    {
        public CharacterStatModifierSO statModifier;
        private float amount;
        public float Amount
        {
            get { return amount; }
            set { this.amount = value; }
        }
    }
}