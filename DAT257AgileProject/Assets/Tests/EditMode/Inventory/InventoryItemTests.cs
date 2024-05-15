using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class InventoryItemTests
{
    [Test]
    public void InventoryItem_GetEmptyItem_ReturnsEmptyItem()
    {
        // Arrange
        InventoryItem inventoryItem;
        // Act
        inventoryItem = InventoryItem.GetEmptyItem();
        // Assert
        Assert.IsTrue(inventoryItem.Quantity == 0);
    }

    [Test]
    public void InventoryItem_GetEmptyItem_IsEmpty()
    {
        // Arrange
        InventoryItem inventoryItem;
        // Act
        inventoryItem = InventoryItem.GetEmptyItem();
        // Assert
        Assert.IsTrue(inventoryItem.IsEmpty);
    }
}