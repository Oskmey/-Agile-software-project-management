using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu(menuName = "Inventory/Items/Trash item")]
    public class TrashItemSO : ItemSO, IDestroyableItem, IItemAction
    {
        public string Actioname => throw new System.NotImplementedException();

        public bool PerformAction(GameObject character)
        {
            throw new System.NotImplementedException();
        }
    }
}