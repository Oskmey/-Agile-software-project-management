using System.Collections;
using System.Collections.Generic;
using Inventory;
using Inventory.Model;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class RecyclingTests : InputTestFixture
{
    private RecyclingManager recyclingManager;
    private PlayerStatsManager playerStatsManager;
    private TrashHandler trashHandler;
    private InventoryManager inventoryManager;
    private Keyboard keyboard;
    private Mouse mouse;

    [SetUp]
    public override void Setup()
    {
        base.Setup();
        keyboard = new Keyboard();
        keyboard = InputSystem.AddDevice<Keyboard>();
        mouse = new Mouse();
        mouse = InputSystem.AddDevice<Mouse>();
        SceneManager.LoadScene("RecyclingTest", LoadSceneMode.Single);
    }

    [TearDown]
    public override void TearDown()
    {
        base.TearDown();
        DataPersistenceManager.Instance.NewGame();
        DataPersistenceManager.Instance.SaveGame();
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    [Description("Testing the recycle input")]
    public IEnumerator RecycleInput_GivenOneTrash_IsRecycled()
    {
        // Wait until the scene is fully loaded
        yield return null;
        DataPersistenceManager.Instance.NewGame();
        yield return null;
        DataPersistenceManager.Instance.SaveGame();
        yield return null;
        DataPersistenceManager.Instance.LoadGame();
        yield return null;

        yield return null;
        inventoryManager = GameObject.FindAnyObjectByType<InventoryManager>();
        playerStatsManager = GameObject.FindAnyObjectByType<PlayerStatsManager>();
        recyclingManager = GameObject.FindGameObjectWithTag("Recycling Manager").GetComponent<RecyclingManager>();
        trashHandler = GameObject.FindAnyObjectByType<TrashHandler>();

        yield return null;
        inventoryManager.ResetInventory();
        inventoryManager.ResetAccessories();
        playerStatsManager.CurrentMoney = 0;
        playerStatsManager.RecycledTrashDictionary.Clear();
        TrashItemSO trashItem = Resources.Load<TrashItemSO>("ScriptableObjects/Items/Common/PET Bottle");

        yield return null;
        InventoryItem trashInventoryItem = new InventoryItem(trashItem, 1, null);
        inventoryManager.InventoryData.AddItem(trashInventoryItem);

        yield return null;

        PressAndRelease(keyboard.rKey);
        yield return null;

        DataPersistenceManager.Instance.SaveGame();
        yield return null;

        Assert.AreEqual(1, playerStatsManager.RecycledTrashDictionary.Count);
        Assert.AreEqual(trashItem.TrashData.MoneyValue, playerStatsManager.CurrentMoney);
    }
}
