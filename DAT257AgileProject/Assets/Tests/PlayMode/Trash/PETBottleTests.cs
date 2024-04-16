using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[TestFixture]
public class PETBottleTests
{
    private GameObject PETBottleObject;
    private TrashScript PETBottleScript;
    private static readonly string PETBottlePrefabPath = "Assets/Prefabs/Trash/PETBottle.prefab";

    [SetUp]
    public void Setup()
    {
        GameObject PETBottlePrefab = AssetDatabase.LoadAssetAtPath(PETBottlePrefabPath, typeof(GameObject)) as GameObject;
        PETBottleObject = Object.Instantiate(PETBottlePrefab);
        PETBottleScript = PETBottleObject.GetComponent<TrashScript>();
    }

    [Test]
    public void PETBottle_HasCorrectTrashType_ReturnsTrue()
    {
        TrashType trashType = PETBottleScript.TrashType;
        Assert.AreEqual(TrashType.PETBottle, trashType);
    }

    [Test]
    public void PETBottle_HasCorrectTrashCategories_ReturnsTrue()
    {
        IReadOnlyList<TrashCategory> correctPETBottleCategories = new List<TrashCategory> { TrashCategory.Plastic };
        IReadOnlyList<TrashCategory> trashCategories = PETBottleScript.TrashCategories;
        CollectionAssert.AreEquivalent(correctPETBottleCategories, trashCategories);
    }

    [Test]
    public void PETBottle_TrashFactsIsNotNull_ReturnsTrue()
    {
        IReadOnlyList<TrashFactData> trashFacts = PETBottleScript.TrashFacts;
        Assert.IsNotNull(trashFacts);
    }

    [Test]
    public void PETBottle_TrashFactsIsNotEmpty_ReturnsTrue()
    {
        IReadOnlyList<TrashFactData> trashFacts = PETBottleScript.TrashFacts;
        Assert.IsNotEmpty(trashFacts);
    }

    [Test]
    public void PETBottle_HasCorrectTrashRarity_ReturnsTrue()
    {
        TrashRarity trashRarity = PETBottleScript.Rarity;
        Assert.AreEqual(TrashRarity.Common, trashRarity);
    }

    [Test]
    public void PETBottle_HasCorrectMoneyValue_ReturnsTrue()
    {
        int moneyValue = PETBottleScript.MoneyValue;
        Assert.AreEqual(12, moneyValue);
    }

    [Test]
    public void PETBottle_IsRecyclable_ReturnsTrue()
    {
        bool isRecyclable = PETBottleScript.IsRecyclable;
        Assert.IsTrue(isRecyclable);
    }

    [Test]
    public void PETBottle_GivenRandomTrashFactIsRun_ReturnsTrashFact()
    {
        TrashFactData trashFact = PETBottleScript.GetRandomTrashFact();
        Assert.IsNotNull(trashFact);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(PETBottleObject);
    }
}
