using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class RecyclingTests : InputTestFixture
{
    private RecyclingManager recyclingManager;
    private PlayerStatsManager playerStatsManager;
    private TrashHandler trashHandler;
    private PlayerInput playerInput;
    private Keyboard keyboard;
    private static readonly string trashBagPrefabPath = "Assets/Prefabs/Trash/TrashBag.prefab";

    [SetUp]
    public override void Setup()
    {
        base.Setup();
        keyboard = new Keyboard();
        keyboard = InputSystem.AddDevice<Keyboard>();
        SceneManager.LoadScene("David's Trash Scene", LoadSceneMode.Single);
    }

    [TearDown]
    public override void TearDown()
    {
        base.TearDown();
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    [Description("Testing the recycle function")]
    public IEnumerator TestRecycleInput()
    {
        // Wait until the scene is fully loaded
        yield return null;

        playerStatsManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatsManager>();
        recyclingManager = GameObject.FindGameObjectWithTag("Recycling Manager").GetComponent<RecyclingManager>();
        trashHandler = GameObject.FindAnyObjectByType<TrashHandler>();

        //GameObject trashBagPrefab = AssetDatabase.LoadAssetAtPath(trashBagPrefabPath, typeof(GameObject)) as GameObject;
        //GameObject trashBagObject = Object.Instantiate(trashBagPrefab);
        //TrashScript trashBagScript = trashBagObject.GetComponent<TrashScript>();

        //recyclingManager.AddTrashToRecycle(trashBagScript);
        //recyclingManager.AddTrashToRecycle(trashBagScript);
        trashHandler.CreateTrash(TrashType.TrashBag);
        yield return null;
        PressAndRelease(keyboard.zKey);
        yield return null;
        trashHandler.CreateTrash(TrashType.TrashBag);
        yield return null;
        PressAndRelease(keyboard.zKey);
        yield return null;

        Debug.Log(recyclingManager.TrashToRecycle.Count);

        PressAndRelease(keyboard.rKey);
        yield return null;

        PressAndRelease(keyboard.rKey);
        yield return null;
        Debug.Log(recyclingManager.TrashToRecycle.Count);

        // destroying the player input to fix an exception logged in the console
        // link: https://forum.unity.com/threads/i-cannot-make-unity-test-framework-work-with-inputtestfixture.1331400/
        Object.Destroy(playerInput);

        Assert.AreEqual(2, playerStatsManager.RecycledTrashList.Count);
        Assert.AreEqual(20, playerStatsManager.Money);
        Assert.IsTrue(recyclingManager.TrashWasRecycled);
    }
}
