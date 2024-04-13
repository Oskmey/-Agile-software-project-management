using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[TestFixture]
public class CigaretteButtsTests
{
    private GameObject cigaretteButtsObject;
    private TrashScript cigaretteButtsScript;
    private static readonly string cigaretteButtsPrefabPath = "Assets/Prefabs/Trash/CigaretteButts.prefab";

    [SetUp]
    public void Setup()
    {
        GameObject cigaretteButtsPrefab = AssetDatabase.LoadAssetAtPath(cigaretteButtsPrefabPath, typeof(GameObject)) as GameObject;
        cigaretteButtsObject = Object.Instantiate(cigaretteButtsPrefab);
        cigaretteButtsScript = cigaretteButtsObject.GetComponent<TrashScript>();
    }

    [Test]
    public void CigaretteButts_HasCorrectTrashType_ReturnsTrue()
    {
        TrashType trashType = cigaretteButtsScript.TrashType;
        Assert.AreEqual(TrashType.CigaretteButt, trashType);
    }

    [Test]
    public void CigaretteButts_HasCorrectTrashCategory_ReturnsTrue()
    {
        IReadOnlyList<TrashCategory> correctCigaretteButtsCategories = new List<TrashCategory> { TrashCategory.Paper, TrashCategory.Plastic };
        IReadOnlyList<TrashCategory> trashCategories = cigaretteButtsScript.TrashCategories;
        CollectionAssert.AreEquivalent(correctCigaretteButtsCategories, trashCategories);
    }

    [Test]
    public void CigaretteButts_TrashFactsIsNotNull_ReturnsTrue()
    {
        IReadOnlyList<TrashFactData> trashFacts = cigaretteButtsScript.TrashFacts;
        Assert.IsNotNull(trashFacts);
    }

    [Test]
    public void CigaretteButts_TrashFactsIsNotEmpty_ReturnsTrue()
    {
        IReadOnlyList<TrashFactData> trashFacts = cigaretteButtsScript.TrashFacts;
        Assert.IsNotEmpty(trashFacts);
    }

    [Test]
    public void CigaretteButts_HasCorrectTrashRarity_ReturnsTrue()
    {
        TrashRarity trashRarity = cigaretteButtsScript.Rarity;
        Assert.AreEqual(TrashRarity.Common, trashRarity);
    }

    [Test]
    public void CigaretteButtsTests_HasCorrectMoneyValue_ReturnsTrue()
    {
        int moneyValue = cigaretteButtsScript.MoneyValue;
        Assert.AreEqual(7, moneyValue);
    }

    [Test]
    public void CigaretteButts_IsRecyclable_ReturnsTrue()
    {
        bool isRecyclable = cigaretteButtsScript.IsRecyclable;
        Assert.IsTrue(isRecyclable);
    }

    [Test]
    public void CigaretteButts_GivenRandomTrashFactIsRun_ReturnsTrashFact()
    {
        TrashFactData trashFact = cigaretteButtsScript.GetRandomTrashFact();
        Assert.IsNotNull(trashFact);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(cigaretteButtsObject);
    }
}