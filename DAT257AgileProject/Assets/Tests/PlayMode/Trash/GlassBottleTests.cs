using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[TestFixture]
public class GlassBottleTests
{
    private GameObject glassBottleObject;
    private TrashScript glassBottleScript;
    private static readonly string glassBottlePrefabPath = "Assets/Prefabs/Trash/GlassBottle.prefab";

    [SetUp]
    public void Setup()
    {
        GameObject glassBottlePrefab = AssetDatabase.LoadAssetAtPath(glassBottlePrefabPath, typeof(GameObject)) as GameObject;
        glassBottleObject = Object.Instantiate(glassBottlePrefab);
        glassBottleScript = glassBottleObject.GetComponent<TrashScript>();
    }

    [Test]
    public void GlassBottle_HasCorrectTrashType_ReturnsTrue()
    {
        TrashType trashType = glassBottleScript.TrashType;
        Assert.AreEqual(TrashType.GlassBottle, trashType);
    }

    [Test]
    public void GlassBottle_HasCorrectTrashCategories_ReturnsTrue()
    {
        IReadOnlyList<TrashCategory> correctGlassBottleCategories = new List<TrashCategory> { TrashCategory.Glass };
        IReadOnlyList<TrashCategory> trashCategories = glassBottleScript.TrashCategories;
        CollectionAssert.AreEquivalent(correctGlassBottleCategories, trashCategories);
    }

    [Test]
    public void GlassBottle_TrashFactsIsNotNull_ReturnsTrue()
    {
        IReadOnlyList<TrashFactData> trashFacts = glassBottleScript.TrashFacts;
        Assert.IsNotNull(trashFacts);
    }

    [Test]
    public void GlassBottle_TrashFactsIsNotEmpty_ReturnsTrue()
    {
        IReadOnlyList<TrashFactData> trashFacts = glassBottleScript.TrashFacts;
        Assert.IsNotEmpty(trashFacts);
    }

    [Test]
    public void GlassBottle_HasCorrectTrashRarity_ReturnsTrue()
    {
        TrashRarity trashRarity = glassBottleScript.Rarity;
        Assert.AreEqual(TrashRarity.Rare, trashRarity);
    }

    [Test]
    public void GlassBottle_HasCorrectMoneyValue_ReturnsTrue()
    {
        int moneyValue = glassBottleScript.MoneyValue;
        Assert.AreEqual(55, moneyValue);
    }

    [Test]
    public void GlassBottle_IsRecyclable_ReturnsTrue()
    {
        bool isRecyclable = glassBottleScript.IsRecyclable;
        Assert.IsTrue(isRecyclable);
    }

    [Test]
    public void GlassBottle_GivenRandomTrashFactIsRun_ReturnsTrashFact()
    {
        TrashFactData trashFact = glassBottleScript.GetRandomTrashFact();
        Assert.IsNotNull(trashFact);
    }

    [Test]
    public void GlassBottle_GivenGlassEffect_HasTransparency()
    {
        GlassEffect glassEffect = glassBottleScript.GetComponent<GlassEffect>();
        Assert.IsNotNull(glassEffect);

        Color glassBottleColor = glassBottleScript.GetComponent<SpriteRenderer>().color;
        Assert.LessOrEqual(glassBottleColor.a, 0.9f);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(glassBottleObject);
    }
} 
