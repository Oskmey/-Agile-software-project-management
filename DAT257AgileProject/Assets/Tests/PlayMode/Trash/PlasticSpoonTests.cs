using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[TestFixture]
public class PlasticSpoonTests
{
    private GameObject plasticSpoonObject;
    private TrashScript plasticSpoonScript;
    private static readonly string plasticSpoonPrefabPath = "Assets/Prefabs/Trash/PlasticSpoon.prefab";

    [SetUp]
    public void Setup()
    {
        GameObject plasticSpoonPrefab = AssetDatabase.LoadAssetAtPath(plasticSpoonPrefabPath, typeof(GameObject)) as GameObject;
        plasticSpoonObject = Object.Instantiate(plasticSpoonPrefab);
        plasticSpoonScript = plasticSpoonObject.GetComponent<TrashScript>();
    }

    [Test]
    public void PlasticSpoon_HasCorrectTrashType_ReturnsTrue()
    {
        TrashType trashType = plasticSpoonScript.TrashType;
        Assert.AreEqual(TrashType.PlasticSpoon, trashType);
    }

    [Test]
    public void PlasticSpoon_HasCorrectTrashCategories_ReturnsTrue()
    {
        IReadOnlyList<TrashCategory> correctPlasticSpoonCategories = new List<TrashCategory> { TrashCategory.Plastic };
        IReadOnlyList<TrashCategory> trashCategories = plasticSpoonScript.TrashCategories;
        CollectionAssert.AreEquivalent(correctPlasticSpoonCategories, trashCategories);
    }

    [Test]
    public void PlasticSpoon_TrashFactsIsNotNull_ReturnsTrue()
    {
        IReadOnlyList<TrashFactData> trashFacts = plasticSpoonScript.TrashFacts;
        Assert.IsNotNull(trashFacts);
    }

    [Test]
    public void PlasticSpoon_TrashFactsIsNotEmpty_ReturnsTrue()
    {
        IReadOnlyList<TrashFactData> trashFacts = plasticSpoonScript.TrashFacts;
        Assert.IsNotEmpty(trashFacts);
    }

    [Test]
    public void PlasticSpoon_HasCorrectTrashRarity_ReturnsTrue()
    {
        TrashRarity trashRarity = plasticSpoonScript.Rarity;
        Assert.AreEqual(TrashRarity.Uncommon, trashRarity);
    }

    [Test]
    public void PlasticSpoon_HasCorrectMoneyValue_ReturnsTrue()
    {
        int moneyValue = plasticSpoonScript.MoneyValue;
        Assert.AreEqual(25, moneyValue);
    }

    [Test]
    public void PlasticSpoon_IsRecyclable_ReturnsTrue()
    {
        bool isRecyclable = plasticSpoonScript.IsRecyclable;
        Assert.IsTrue(isRecyclable);
    }

    [Test]
    public void PlasticSpoon_GivenRandomTrashFactIsRun_ReturnsTrashFact()
    {
        TrashFactData trashFact = plasticSpoonScript.GetRandomTrashFact();
        Assert.IsNotNull(trashFact);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(plasticSpoonObject);
    }
}