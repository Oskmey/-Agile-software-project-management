using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    public interface IItemAction
    {
        public string ActionName { get; }
        //public AudioClip actionSFX { get; set; }
        bool PerformAction(GameObject character, List<ItemParameter> itemState);
    }
}