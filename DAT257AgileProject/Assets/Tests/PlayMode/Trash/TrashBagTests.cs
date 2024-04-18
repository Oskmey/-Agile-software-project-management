using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// Was originally meant to be in edit mode, but moved to play mode
// because awake is not run in edit mode
[TestFixture]
public class TrashBagTests
{
    private GameObject trashBagObject;
    private TrashScript trashBagScript;
    private static readonly string trashBagPrefabPath = "Assets/Prefabs/Trash/TrashBag.prefab";

    [SetUp]
    public void Setup()
    {
        GameObject trashBagPrefab = AssetDatabase.LoadAssetAtPath(trashBagPrefabPath, typeof(GameObject)) as GameObject;
        trashBagObject = Object.Instantiate(trashBagPrefab);
        trashBagScript = trashBagObject.GetComponent<TrashScript>();
    }

    [Test]
    public void TrashBag_HasCorrectTrashType_ReturnsTrue()
    {
        TrashType trashType = trashBagScript.TrashType;
        Assert.AreEqual(TrashType.TrashBag, trashType);
    }

    [Test]
    public void TrashBag_HasCorrectTrashCategories_ReturnsTrue()
    {
        IReadOnlyList<TrashCategory> correctTrashBagCategories = new List<TrashCategory> { TrashCategory.Plastic };
        IReadOnlyList<TrashCategory> trashCategories = trashBagScript.TrashCategories;
        CollectionAssert.AreEquivalent(correctTrashBagCategories, trashCategories);
    }

    [Test]
    public void TrashBag_TrashFactsIsNotNull_ReturnsTrue()
    {
        IReadOnlyList<TrashFactData> trashFacts = trashBagScript.TrashFacts;
        Assert.IsNotNull(trashFacts);
    }

    [Test]
    public void TrashBag_TrashFactsIsNotEmpty_ReturnsTrue()
    {
        IReadOnlyList<TrashFactData> trashFacts = trashBagScript.TrashFacts;
        Assert.IsNotEmpty(trashFacts);
    }

    [Test]
    public void TrashBag_HasCorrectTrashRarity_ReturnsTrue()
    {
        TrashRarity trashRarity = trashBagScript.Rarity;
        Assert.AreEqual(TrashRarity.Common, trashRarity);
    }

    [Test]
    public void TrashBag_HasCorrectMoneyValue_ReturnsTrue()
    {
        int moneyValue = trashBagScript.MoneyValue;
        Assert.AreEqual(10, moneyValue);
    }

    [Test]
    public void TrashBag_IsRecyclable_ReturnsTrue()
    {
        bool isRecyclable = trashBagScript.IsRecyclable;
        Assert.IsTrue(isRecyclable);
    }

    [Test]
    public void TrashBag_GivenRandomTrashFactIsRun_ReturnsTrashFact()
    {
        TrashFactData trashFact = trashBagScript.GetRandomTrashFact();
        Assert.IsNotNull(trashFact);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(trashBagObject);
    }
}
