using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class InventorySOTests
{
    private static readonly InventorySO inventoryData = Resources.Load<InventorySO>("ScriptableObjects/Inventory/PlayerInventory");

    [SetUp]
    public void Setup()
    {
        inventoryData.Initialize();
    }

    [TearDown]
    public void TearDown()
    {
        inventoryData.Initialize();
    }

    [Test]
    public void Inventory_WhenInit_HasFilledEverySlot()
    {
        inventoryData.Initialize();

        int inventorySlots = inventoryData.InventoryItems.Count;
        int initInventorySize = inventoryData.Size;
        Assert.AreEqual(initInventorySize, inventorySlots);
    }
    [Test]
    public void Inventory_WhenAddingInventoryItem_IsPlacedAtAvailableSlot()
    {
        TrashItemSO trashItem = Resources.Load<TrashItemSO>("ScriptableObjects/Items/Common/PET Bottle");
        InventoryItem trashInventoryItem = new InventoryItem(trashItem, 1, null);
        inventoryData.AddItem(trashInventoryItem);
        InventoryItem firstItem = inventoryData.GetItemAt(0);

        Assert.AreEqual(1, firstItem.Quantity);
    }

    [Test]
    public void Inventory_AddingInventoryItemAtIndex_IsPlacedAtIndex()
    {
        TrashItemSO trashItem = Resources.Load<TrashItemSO>("ScriptableObjects/Items/Common/PET Bottle");
        
        InventoryItem trashInventoryItem = new InventoryItem(trashItem, 1, null);
        inventoryData.AddItemAt(trashInventoryItem, 1);
        InventoryItem firstItem = inventoryData.GetItemAt(1);

        Assert.AreEqual(1, firstItem.Quantity);
    }

    [Test]
    public void Inventory_WhenAddingTwoSimilarInventoryItem_IsStacked()
    {
        TrashItemSO trashItem = Resources.Load<TrashItemSO>("ScriptableObjects/Items/Common/PET Bottle");
        InventoryItem trashInventoryItem_1 = new InventoryItem(trashItem, 1, null);
        InventoryItem trashInventoryItem_2 = new InventoryItem(trashItem, 1, null);

        inventoryData.AddItem(trashInventoryItem_1);
        inventoryData.AddItem(trashInventoryItem_2);
        InventoryItem firstItem = inventoryData.GetItemAt(0);

        Assert.AreEqual(2, firstItem.Quantity);
    }

    [Test]
    public void Inventory_RemovingItem_MakesSlotEmpty()
    {
        TrashItemSO trashItem_1 = Resources.Load<TrashItemSO>("ScriptableObjects/Items/Common/PET Bottle");

        InventoryItem trashInventoryItem_1 = new InventoryItem(trashItem_1, 1, null);
        inventoryData.AddItemAt(trashInventoryItem_1, 0);
        inventoryData.RemoveItem(0, 1);
        InventoryItem firstItem = inventoryData.GetItemAt(0);

        Assert.IsTrue(firstItem.IsEmpty);
    }

    [Test]
    public void Inventory_SwappingItems_SwitchesItemAtIndex()
    {
        TrashItemSO trashItem_1 = Resources.Load<TrashItemSO>("ScriptableObjects/Items/Common/PET Bottle");
        TrashItemSO trashItem_2 = Resources.Load<TrashItemSO>("ScriptableObjects/Items/Common/Trash Bag");

        InventoryItem trashInventoryItem_1 = new InventoryItem(trashItem_1, 1, null);
        inventoryData.AddItemAt(trashInventoryItem_1, 0);
        InventoryItem firstItem = inventoryData.GetItemAt(0);

        InventoryItem trashInventoryItem_2 = new InventoryItem(trashItem_2, 1, null);
        inventoryData.AddItemAt(trashInventoryItem_2, 1);
        InventoryItem secondItem = inventoryData.GetItemAt(1);
        int currentIndex = 0;
        int destinationIndex = 1;
        inventoryData.SwapItems(currentIndex, destinationIndex);


        Assert.AreEqual(inventoryData.GetItemAt(1).Item.Name, firstItem.Item.Name);
        Assert.AreEqual(inventoryData.GetItemAt(0).Item.Name, secondItem.Item.Name);
    }

    [Test]
    public void Inventory_GetAndRemoveRecyclableTrashItems_ReturnsValidTrashAndRemovesFromInventory()
    {
        TrashItemSO trashItem_1 = Resources.Load<TrashItemSO>("ScriptableObjects/Items/Common/PET Bottle");

        InventoryItem trashInventoryItem_1 = new InventoryItem(trashItem_1, 1, null);
        inventoryData.AddItem(trashInventoryItem_1);

        Dictionary<int, InventoryItem> inventoryState = inventoryData.GetCurrentInventoryState();
        bool containsNoItems = false;

        if (inventoryState.Count > 0)
        {
            containsNoItems = true;
        }

        List<TrashItemSO> trashList = inventoryData.GetAndRemoveRecyclableTrashItems();

        Assert.IsTrue(containsNoItems);
        Assert.GreaterOrEqual(trashList.Count, 1);
    }

    [Test]
    public void Inventory_GetCurrentState_ReturnsNotEmptyItems()
    {
        TrashItemSO trashItem_1 = Resources.Load<TrashItemSO>("ScriptableObjects/Items/Common/PET Bottle");

        InventoryItem trashInventoryItem_1 = new InventoryItem(trashItem_1, 1, null);
        inventoryData.AddItemAt(trashInventoryItem_1, 0);

        Dictionary<int, InventoryItem> inventoryState = inventoryData.GetCurrentInventoryState();
        bool containsNoEmpty = true;
        foreach (var item in inventoryState)
        {
            if (item.Value.IsEmpty)
            {
                containsNoEmpty = false;
            }
        }

        Assert.IsTrue(containsNoEmpty);
    }

    [Test]
    public void Inventory_CheckInventoryFullWhenInit_ReturnsFalse()
    {
        TrashItemSO trashItem_1 = Resources.Load<TrashItemSO>("ScriptableObjects/Items/Common/PET Bottle");

        InventoryItem trashInventoryItem_1 = new InventoryItem(trashItem_1, 1, null);
        inventoryData.AddItem(trashInventoryItem_1);

        Assert.IsFalse(inventoryData.IsInventoryFull());
    }
}
