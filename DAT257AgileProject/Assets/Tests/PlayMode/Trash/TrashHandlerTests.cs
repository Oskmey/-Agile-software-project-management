using NUnit.Framework;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.TestTools;

public class TrashHandlerTests : InputTestFixture
{
    private GameObject trashHandlerObject;
    private GameObject gameplayHudObject;
    private TrashHandler trashHandler;
    private PlayerInput playerInput;
    private Mouse mouse;
    private Keyboard keyboard;
    private static readonly string trashHandlerPrefabPath = "Assets/Prefabs/Utility/TrashHandler.prefab";
    private static readonly string gameplayHudPrefabPath = "Assets/Prefabs/UI/GameplayHud.prefab";

    [SetUp]
    public override void Setup()
    {
        base.Setup();
        keyboard = new Keyboard();
        keyboard = InputSystem.AddDevice<Keyboard>();
        mouse = new Mouse();
        mouse = InputSystem.AddDevice<Mouse>();

        GameObject gameplayHudPrefab = AssetDatabase.LoadAssetAtPath(gameplayHudPrefabPath, typeof(GameObject)) as GameObject;
        gameplayHudObject = Object.Instantiate(gameplayHudPrefab);
        gameplayHudObject.GetComponent<GameplayHudHandler>().TryFindInfoPanel();

        GameObject trashHandlerPrefab = AssetDatabase.LoadAssetAtPath(trashHandlerPrefabPath, typeof(GameObject)) as GameObject;
        trashHandlerObject = Object.Instantiate(trashHandlerPrefab);
        
        trashHandler = trashHandlerObject.GetComponent<TrashHandler>();
        trashHandler.TryFindGameplayHudHandler();
        playerInput = trashHandlerObject.GetComponent<PlayerInput>();
    }

    [Test]
    public void TrashHandler_IsNotNull_ReturnsTrue()
    {
        Assert.IsNotNull(trashHandler);
    }

    [Test]
    public void CreateTrash_WhenCalledWithTrashType_ShouldCreateTrash()
    {
        trashHandler.CreateTrash(TrashType.TrashBag);
        Assert.IsNotNull(trashHandler.CurrentTrashObject);
    }

    [Test]
    public void CreateTrash_WhenCalledWithTrashTypeAndPosition_ShouldCreateTrashAtPosition()
    {
        Vector2 position = new(1, 1);
        trashHandler.CreateTrash(TrashType.TrashBag, position);
        float positionX = trashHandler.CurrentTrashObject.transform.position.x;
        float positionY = trashHandler.CurrentTrashObject.transform.position.y;
        // Have to convert to Vector2 because a gameobject's position is a Vector3 by default
        Vector2 actualPosition = new(positionX, positionY);
        Assert.AreEqual(position, actualPosition);
    }

    [UnityTest]
    public IEnumerator DestroyTrash_WhenCalled_ShouldDestroyTrash()
    {
        trashHandler.CreateTrash(TrashType.TrashBag);
        trashHandler.DestroyTrash();
        // Wait until the next frame
        yield return null;
        
        Assert.IsTrue(trashHandler.CurrentTrashObject.Equals(null));
    }

    // Tried creating a test for input, but couldn't make it work. 
    //[UnityTest]
    //public IEnumerator Update_WhenHideTrashInfoPanelActionTriggered_ShouldDestroyTrash()
    //{
    //    trashHandler.CreateTrash(TrashType.TrashBag);
    //    playerInput.actions["HideTrashInfoPanel"].Enable();
    //    PressAndRelease(mouse.leftButton);
    //    yield return null;
    //    playerInput.actions["HideTrashInfoPanel"].Disable();
    //    Object.Destroy(playerInput);
    //    Assert.IsTrue(trashHandler.CurrentTrashObject.Equals(null), $"Current Trash Object was {trashHandler.CurrentTrashObject}");
    //}

    [TearDown]
    public override void TearDown()
    {
        base.TearDown();
        Object.DestroyImmediate(trashHandlerObject);
        Object.DestroyImmediate(gameplayHudObject);
        Object.DestroyImmediate(trashHandler);
        Object.DestroyImmediate(playerInput);
    }
}