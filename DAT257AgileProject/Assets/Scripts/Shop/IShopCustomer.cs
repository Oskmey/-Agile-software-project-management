using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShopCustomer 
{
    void BoughtItem(PurchasableItem.Type type);
}
