using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[TestFixture]
public class ElectricScooterTests
{
    private GameObject electricScooterObject;
    private TrashScript electricScooterScript;
    private static readonly string electricScooterPrefabPath = "Assets/Prefabs/Trash/ElectricScooter.prefab";

    [SetUp]
    public void Setup()
    {
        GameObject electricScooterPrefab = AssetDatabase.LoadAssetAtPath(electricScooterPrefabPath, typeof(GameObject)) as GameObject;
        electricScooterObject = Object.Instantiate(electricScooterPrefab);
        electricScooterScript = electricScooterObject.GetComponent<TrashScript>();
    }

    [Test]
    public void ElectricScooter_HasCorrectTrashType_ReturnsTrue()
    {
        TrashType trashType = electricScooterScript.TrashType;
        Assert.AreEqual(TrashType.ElectricScooter, trashType);
    }

    [Test]
    public void ElectricScooter_HasCorrectTrashCategories_ReturnsTrue()
    {
        IReadOnlyList<TrashCategory> correctElectricScooterCategories = new List<TrashCategory> { TrashCategory.Metal, TrashCategory.Electronic };
        IReadOnlyList<TrashCategory> trashCategories = electricScooterScript.TrashCategories;
        CollectionAssert.AreEquivalent(correctElectricScooterCategories, trashCategories);
    }

    [Test]
    public void ElectricScooter_TrashFactsIsNotNull_ReturnsTrue()
    {
        IReadOnlyList<TrashFactData> trashFacts = electricScooterScript.TrashFacts;
        Assert.IsNotNull(trashFacts);
    }

    [Test]
    public void ElectricScooter_TrashFactsIsNotEmpty_ReturnsTrue()
    {
        IReadOnlyList<TrashFactData> trashFacts = electricScooterScript.TrashFacts;
        Assert.IsNotEmpty(trashFacts);
    }

    [Test]
    public void ElectricScooter_HasCorrectTrashRarity_ReturnsTrue()
    {
        TrashRarity trashRarity = electricScooterScript.Rarity;
        Assert.AreEqual(TrashRarity.Legendary, trashRarity);
    }

    [Test]
    public void ElectricScooter_HasCorrectMoneyValue_ReturnsTrue()
    {
        int moneyValue = electricScooterScript.MoneyValue;
        Assert.AreEqual(1000, moneyValue);
    }

    [Test]

    public void ElectricScooter_IsRecyclable_ReturnsTrue()
    {
        bool isRecyclable = electricScooterScript.IsRecyclable;
        Assert.IsTrue(isRecyclable);
    }

    [Test]
    public void ElectricScooter_GivenRandomTrashFactIsRun_ReturnsTrashFact()
    {
        TrashFactData trashFact = electricScooterScript.GetRandomTrashFact();
        Assert.IsNotNull(trashFact);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(electricScooterObject);
    }

}