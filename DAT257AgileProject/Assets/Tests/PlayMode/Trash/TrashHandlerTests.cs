using NUnit.Framework;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class TrashHandlerTests : InputTestFixture
{
    private TrashHandler trashHandler;
    private Mouse mouse;
    private PlayerInput playerInput;
    private Keyboard keyboard;

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

    [Test]
    public void TrashHandler_IsNotNull_ReturnsTrue()
    {
        trashHandler = GameObject.FindAnyObjectByType<TrashHandler>();
        Assert.IsNotNull(trashHandler);
    }

    [UnityTest]
    public IEnumerator CreateTrash_WhenCalledWithTrashType_ShouldCreateTrash()
    {
        yield return null;
        trashHandler = GameObject.FindAnyObjectByType<TrashHandler>();
        yield return null;
        trashHandler.CreateTrash(TrashType.TrashBag);
        yield return null;
        Assert.IsNotNull(trashHandler.CurrentTrashObject);
    }

    [UnityTest]
    public IEnumerator CreateTrash_WhenCalledWithTrashTypeAndPosition_ShouldCreateTrashAtPosition()
    {
        yield return null;
        trashHandler = GameObject.FindAnyObjectByType<TrashHandler>();
        Vector2 position = new(1, 1);
        trashHandler.CreateTrash(TrashType.TrashBag, position);
        yield return null;
        float positionX = trashHandler.CurrentTrashObject.transform.position.x;
        float positionY = trashHandler.CurrentTrashObject.transform.position.y;
        // Have to convert to Vector2 because a gameobject's position is a Vector3 by default
        Vector2 actualPosition = new(positionX, positionY);
        Assert.AreEqual(position, actualPosition);
    }

    [UnityTest]
    public IEnumerator CreateRandomTrash_WhenCalledWithTrashRarityAndPosition_ShouldCreateTrash()
    {
        yield return null;
        trashHandler = GameObject.FindAnyObjectByType<TrashHandler>();
        Vector2 position = new(1, 1);
        trashHandler.CreateRandomTrash(TrashRarity.Common, position);
        yield return null;
        Assert.IsNotNull(trashHandler.CurrentTrashObject);
    }

    [UnityTest]
    public IEnumerator CreateRandomTrash_WhenCalledWithTrashRarityAndPosition_ShouldCreateTrashAtPosition()
    {
        yield return null;
        trashHandler = GameObject.FindAnyObjectByType<TrashHandler>();
        playerInput = GameObject.FindAnyObjectByType<PlayerInput>();
        yield return null;
        Vector2 position = new(1, 1);
        trashHandler.CreateRandomTrash(TrashRarity.Common, position);
        yield return null;
        float positionX = trashHandler.CurrentTrashObject.transform.position.x;
        float positionY = trashHandler.CurrentTrashObject.transform.position.y;
        // Have to convert to Vector2 because a gameobject's position is a Vector3 by default
        Vector2 actualPosition = new(positionX, positionY);
        Object.Destroy(playerInput);
        Assert.AreEqual(position, actualPosition);
    }

    [UnityTest]
    public IEnumerator DestroyTrash_WhenCalled_ShouldDestroyTrash()
    {
        yield return null;
        trashHandler = GameObject.FindAnyObjectByType<TrashHandler>();
        yield return null;
        trashHandler.CreateTrash(TrashType.TrashBag);
        yield return null;
        trashHandler.DestroyTrash();
        yield return null;
        bool trashNull = false;

        if(trashHandler.CurrentTrashObject != null)
        {
            trashNull = false;
        }
        else
        {
            trashNull = true;
        }
        
        Assert.IsTrue(trashNull);
    }

    [UnityTest]
    public IEnumerator Update_WhenHideTrashInfoPanelActionTriggered_ShouldDestroyTrash()
    {
        yield return null;
        trashHandler = GameObject.FindAnyObjectByType<TrashHandler>();
        playerInput = GameObject.FindAnyObjectByType<PlayerInput>();
        yield return null;
        trashHandler.CreateTrash(TrashType.TrashBag);
        yield return null;
        playerInput.actions["HideTrashInfoPanel"].Enable();
        PressAndRelease(mouse.leftButton);
        yield return null;
        playerInput.actions["HideTrashInfoPanel"].Disable();
        yield return null;
        Object.Destroy(playerInput);
        bool trashNull = false;

        if (trashHandler.CurrentTrashObject != null)
        {
            trashNull = false;
        }
        else
        {
            trashNull = true;
        }
        Assert.IsTrue(trashNull, $"Current Trash Object was {trashHandler.CurrentTrashObject}");
    }

    [TearDown]
    public override void TearDown()
    {
        base.TearDown();
        trashHandler = null;
    }
}