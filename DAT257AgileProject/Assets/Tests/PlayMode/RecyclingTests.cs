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
    private Keyboard keyboard;
    private Mouse mouse;

    [SetUp]
    public override void Setup()
    {
        base.Setup();
        keyboard = new Keyboard();
        keyboard = InputSystem.AddDevice<Keyboard>();
        mouse = new Mouse();
        mouse = InputSystem.AddDevice<Mouse>();
        SceneManager.LoadScene("RecyclingTest", LoadSceneMode.Single);
    }

    [TearDown]
    public override void TearDown()
    {
        base.TearDown();
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    [Description("Testing the recycle input")]
    public IEnumerator RecycleInput_GivenTwoTrash_IsAllRecycled()
    {
        // Wait until the scene is fully loaded
        yield return null;

        playerStatsManager = GameObject.FindAnyObjectByType<PlayerStatsManager>();
        recyclingManager = GameObject.FindGameObjectWithTag("Recycling Manager").GetComponent<RecyclingManager>();
        trashHandler = GameObject.FindAnyObjectByType<TrashHandler>();

        yield return null;
        trashHandler.CreateTrash(TrashType.TrashBag);
        yield return null;

        trashHandler.DestroyTrash();
        yield return null;

        PressAndRelease(keyboard.rKey);
        yield return null;

        trashHandler.CreateTrash(TrashType.TrashBag);
        yield return null;

        trashHandler.DestroyTrash();
        yield return null;

        PressAndRelease(keyboard.rKey);
        yield return null;

        // destroying the player input to fix an exception logged in the console
        // link: https://forum.unity.com/threads/i-cannot-make-unity-test-framework-work-with-inputtestfixture.1331400/
        // Object.Destroy(recyclingPlayerInput);

        Assert.AreEqual(2, playerStatsManager.RecycledTrashDictionary.Count);
        Assert.AreEqual(20, playerStatsManager.Money);
        Assert.IsTrue(recyclingManager.TrashWasRecycled);
    }
}
