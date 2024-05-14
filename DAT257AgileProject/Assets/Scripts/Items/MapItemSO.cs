using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu(menuName = "Inventory/Items/mapItemSO")]
    public class mapItemSO : ItemSO, IDestroyableItem
    {
        [SerializeField]
        private string mapName;

        [SerializeField]
        private AccessorySO accessory;
        public AccessorySO Accessory => accessory;

        public string MapName => mapName;

        public void Awake()
        {
            Name = accessory.AccessoryName;
            //trashModifierData.Amount = trashData.MoneyValue;
        }
    }
}