using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[TestFixture]
public class CarTireTests
{
    private GameObject carTireObject;
    private TrashScript carTireScript;
    private static readonly string carTirePrefabPath = "Assets/Prefabs/Trash/CarTire.prefab";

    [SetUp]
    public void Setup()
    {
        GameObject carTirePrefab = (GameObject)AssetDatabase.LoadAssetAtPath(carTirePrefabPath, typeof(GameObject));
        carTireObject = Object.Instantiate(carTirePrefab);
        carTireScript = carTireObject.GetComponent<TrashScript>();
    }

    [Test]
    public void CarTire_HasCorrectTrashType_ReturnsTrue()
    {
        TrashType trashType = carTireScript.TrashType;
        Assert.AreEqual(TrashType.CarTire, trashType);
    }

    [Test]
    public void CarTire_HasCorrectTrashCategories_ReturnsTrue()
    {
        IReadOnlyList<TrashCategory> correctCarTireCategories = new List<TrashCategory> { TrashCategory.Rubber, TrashCategory.Plastic };
        IReadOnlyList<TrashCategory> trashCategories = carTireScript.TrashCategories;
        CollectionAssert.AreEquivalent(correctCarTireCategories, trashCategories);
    }

    [Test]
    public void CarTire_TrashFactsIsNotNull_ReturnsTrue()
    {
        IReadOnlyList<TrashFactData> trashFacts = carTireScript.TrashFacts;
        Assert.IsNotNull(trashFacts);
    }

    [Test]
    public void CarTire_TrashFactsIsNotEmpty_ReturnsTrue()
    {
        IReadOnlyList<TrashFactData> trashFacts = carTireScript.TrashFacts;
        Assert.IsNotEmpty(trashFacts);
    }

    [Test]
    public void CarTire_HasCorrectTrashRarity_ReturnsTrue()
    {
        TrashRarity trashRarity = carTireScript.Rarity;
        Assert.AreEqual(TrashRarity.Epic, trashRarity);
    }

    [Test]
    public void CarTire_HasCorrectMoneyValue_ReturnsTrue()
    {
        int moneyValue = carTireScript.MoneyValue;
        Assert.AreEqual(250, moneyValue);
    }

    [Test]
    public void CarTire_IsRecyclable_ReturnsTrue()
    {
        bool isRecyclable = carTireScript.IsRecyclable;
        Assert.IsTrue(isRecyclable);
    }

    [Test]
    public void CarTire_GivenRandomTrashFactIsRun_ReturnsTrashFact()
    {
        TrashFactData trashFact = carTireScript.GetRandomTrashFact();
        Assert.IsNotNull(trashFact);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(carTireObject);
    }
}
