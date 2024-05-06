using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [Serializable]
    public struct ItemParameter : IEquatable<ItemParameter>
    {
        [SerializeField]
        private ItemParameterSO itemParameter;
        [SerializeField]
        private float value;

        public bool Equals(ItemParameter other)
        {
            return itemParameter == other.itemParameter;
        }

        public readonly ItemParameterSO GetItemParameter()
        {
            return itemParameter;
        }

        public readonly float Value => value;
    }
}