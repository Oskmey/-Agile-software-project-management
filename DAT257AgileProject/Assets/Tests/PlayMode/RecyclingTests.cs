using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TestTools;

public class RecyclingTests : InputTestFixture
{
    private RecyclingManager recyclingManager;
    private PlayerStatsManager playerStatsManager;
    private RecyclingMachine recyclingMachine;
    private PlayerController playerControlsSim;
    private PlayerInput playerInput;
    private Keyboard keyboard;

    private GameObject playerGameObject;
    private GameObject recyclingMachineGameObject;
    private GameObject recyclingManagerGameObject;

    [SetUp]
    public override void Setup()
    {
        base.Setup();
        keyboard = new Keyboard();
        keyboard = InputSystem.AddDevice<Keyboard>();

        GameObject recyclingMachineGameObject = new();
        recyclingMachineGameObject.AddComponent<RecyclingMachine>();
        recyclingMachineGameObject.tag = "Recycle Machine";
        recyclingMachine = recyclingMachineGameObject.GetComponent<RecyclingMachine>();

        GameObject recyclingManagerGameObject = new();
        recyclingManagerGameObject.AddComponent<RecyclingManager>();
        recyclingManagerGameObject.tag = "Recycling Manager";
        recyclingManager = recyclingManagerGameObject.GetComponent<RecyclingManager>();

        GameObject playerGameObject = new();
        playerGameObject.tag = "Player";
        playerGameObject.AddComponent<PlayerStatsManager>();
        playerStatsManager = playerGameObject.GetComponent<PlayerStatsManager>();

        playerGameObject.AddComponent<PlayerController>();
        playerGameObject.AddComponent<PlayerInput>();
        playerGameObject.GetComponent<PlayerInput>().actions = Resources.Load<InputActionAsset>("Inputs/PlayerInputActions");
        playerInput = playerGameObject.GetComponent<PlayerInput>();
        playerControlsSim = playerGameObject.GetComponent<PlayerController>();
    }

    [TearDown]
    public override void TearDown()
    {
        base.TearDown();
        Object.Destroy(recyclingManager);
        Object.Destroy(playerStatsManager);
        Object.Destroy(recyclingMachine);
        Object.Destroy(playerControlsSim);

        Object.Destroy(playerGameObject);
        Object.Destroy(recyclingMachineGameObject);
        Object.Destroy(recyclingManagerGameObject);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    [Description("Testing the recycle function")]
    public IEnumerator TestRecycleInput()
    {
        PressAndRelease(keyboard.spaceKey);
        yield return null;

        PressAndRelease(keyboard.spaceKey);
        yield return null;

        // destroying the player input to fix an exception logged in the console
        // link: https://forum.unity.com/threads/i-cannot-make-unity-test-framework-work-with-inputtestfixture.1331400/
        Object.Destroy(playerInput);

        Assert.AreEqual(2, playerStatsManager.RecycledTrashList.Count);
        Assert.AreEqual(20, playerStatsManager.Money);
        Assert.IsTrue(recyclingManager.TrashWasRecycled);
    }
}
