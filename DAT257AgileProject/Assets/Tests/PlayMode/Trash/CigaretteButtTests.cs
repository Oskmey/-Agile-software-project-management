using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[TestFixture]
public class CigaretteButtTests
{
    private GameObject cigaretteButtObject;
    private TrashScript cigaretteButtScript;
    private static readonly string cigaretteButtPrefabPath = "Assets/Prefabs/Trash/CigaretteButt.prefab";

    [SetUp]
    public void Setup()
    {
        GameObject cigaretteButtPrefab = AssetDatabase.LoadAssetAtPath(cigaretteButtPrefabPath, typeof(GameObject)) as GameObject;
        cigaretteButtObject = Object.Instantiate(cigaretteButtPrefab);
        cigaretteButtScript = cigaretteButtObject.GetComponent<TrashScript>();
    }

    [Test]
    public void CigaretteButt_HasCorrectTrashType_ReturnsTrue()
    {
        TrashType trashType = cigaretteButtScript.TrashType;
        Assert.AreEqual(TrashType.CigaretteButt, trashType);
    }

    [Test]
    public void CigaretteButt_HasCorrectTrashCategories_ReturnsTrue()
    {
        IReadOnlyList<TrashCategory> correctCigaretteButtsCategories = new List<TrashCategory> { TrashCategory.Paper, TrashCategory.Plastic };
        IReadOnlyList<TrashCategory> trashCategories = cigaretteButtScript.TrashCategories;
        CollectionAssert.AreEquivalent(correctCigaretteButtsCategories, trashCategories);
    }

    [Test]
    public void CigaretteButt_TrashFactsIsNotNull_ReturnsTrue()
    {
        IReadOnlyList<TrashFactData> trashFacts = cigaretteButtScript.TrashFacts;
        Assert.IsNotNull(trashFacts);
    }

    [Test]
    public void CigaretteButt_TrashFactsIsNotEmpty_ReturnsTrue()
    {
        IReadOnlyList<TrashFactData> trashFacts = cigaretteButtScript.TrashFacts;
        Assert.IsNotEmpty(trashFacts);
    }

    [Test]
    public void CigaretteButt_HasCorrectTrashRarity_ReturnsTrue()
    {
        TrashRarity trashRarity = cigaretteButtScript.Rarity;
        Assert.AreEqual(TrashRarity.Common, trashRarity);
    }

    [Test]
    public void CigaretteButt_HasCorrectMoneyValue_ReturnsTrue()
    {
        int moneyValue = cigaretteButtScript.MoneyValue;
        Assert.AreEqual(7, moneyValue);
    }

    [Test]
    public void CigaretteButt_IsRecyclable_ReturnsTrue()
    {
        bool isRecyclable = cigaretteButtScript.IsRecyclable;
        Assert.IsTrue(isRecyclable);
    }

    [Test]
    public void CigaretteButt_GivenRandomTrashFactIsRun_ReturnsTrashFact()
    {
        TrashFactData trashFact = cigaretteButtScript.GetRandomTrashFact();
        Assert.IsNotNull(trashFact);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(cigaretteButtObject);
    }
}