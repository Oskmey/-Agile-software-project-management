using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[TestFixture]
public class TrashFactoryTests
{
    private GameObject trashHandlerObject;
    private TrashFactory trashFactory;
    private static readonly string trashHandlerPrefabPath = "Assets/Prefabs/Utility/TrashHandler.prefab";

    [SetUp]
    public void Setup()
    {
        GameObject trashHandlerPrefab = AssetDatabase.LoadAssetAtPath(trashHandlerPrefabPath, typeof(GameObject)) as GameObject;
        trashHandlerObject = UnityEngine.Object.Instantiate(trashHandlerPrefab);
        trashFactory = trashHandlerObject.GetComponent<TrashFactory>();
    }

    private static IEnumerable<TrashType> TrashTypeTestCases
    {
        get { return Enum.GetValues(typeof(TrashType)).Cast<TrashType>(); }
    }

    [Test]
    public void TrashFactory_HasTrashPrefabs_ReturnsTrue()
    {
        Assert.IsNotNull(TrashFactory.TrashPrefabs);
    }

    [Test]
    public void TrashFactory_TrashPrefabsIsNotEmpty_ReturnsTrue()
    {
        Assert.IsNotEmpty(TrashFactory.TrashPrefabs);
    }

    [Test]
    public void TrashFactory_CreateTrash_ReturnsTrashPrefab()
    {
        GameObject trashPrefab = TrashFactory.CreateTrash(TrashType.TrashBag);
        Assert.IsNotNull(trashPrefab);
    }

    [Test, TestCaseSource(nameof(TrashTypeTestCases))]
    public void EachTrashType_CanBeCreated(TrashType trashType)
    {
        GameObject createdTrash = TrashFactory.CreateTrash(trashType);
        Assert.IsNotNull(createdTrash);
    }

    [Test, TestCaseSource(nameof(TrashTypeTestCases))]
    public void EachTrashType_CanBeCreatedWithCorrectType(TrashType trashType)
    {
        GameObject createdTrash = TrashFactory.CreateTrash(trashType);
        TrashScript createdTrashScript = createdTrash.GetComponent<TrashScript>();
        Assert.AreEqual(trashType, createdTrashScript.TrashType);
    }

    [TearDown]
    public void TearDown()
    {
        UnityEngine.Object.DestroyImmediate(trashHandlerObject);
    }
}