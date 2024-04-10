using NUnit.Framework;
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
        trashHandlerObject = Object.Instantiate(trashHandlerPrefab);
        trashFactory = trashHandlerObject.GetComponent<TrashFactory>();
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

    [Test]
    public void TrashFactory_CreateTrashBag_ReturnsTrashBagPrefab()
    {
        GameObject trashBagPrefab = TrashFactory.CreateTrash(TrashType.TrashBag);
        TrashScript trashBagScript = trashBagPrefab.GetComponent<TrashScript>();
        Assert.AreEqual(TrashType.TrashBag, trashBagScript.TrashType);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(trashHandlerObject);
    }
}