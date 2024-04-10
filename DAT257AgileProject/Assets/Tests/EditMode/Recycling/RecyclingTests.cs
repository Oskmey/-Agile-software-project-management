using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class RecyclingTests
{
    private RecyclingMachine recyclingMachine;
    [SetUp]
    public void Setup()
    {
        GameObject recyclingMachineGameObject = new GameObject();
        recyclingMachineGameObject.AddComponent<RecyclingMachine>();
        recyclingMachine = recyclingMachineGameObject.GetComponent<RecyclingMachine>();
    }
    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(recyclingMachine);
    }
    // A Test behaves as an ordinary method
    [Test]
    public void Recycling_GivenNonRecycableTrash_ReturnsFalse()
    {
        // Assign
        GameObject trash = new();
        trash.AddComponent<RecyclingMachine.Trash>();

        // Act
        bool isRecycable = recyclingMachine.IsTrashRecyclable(trash);

        // Assert
        Assert.IsFalse(isRecycable);
    }
    [Test]
    public void Recycling_GivenRecycableTrash_ReturnsTrue()
    {
        // Assign
        GameObject trash = new();
        trash.AddComponent<RecyclingMachine.RecycableTrash>();

        // Act
        bool isRecycable = recyclingMachine.IsTrashRecyclable(trash);

        // Assert
        Assert.IsTrue(isRecycable);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator RecyclingTestsWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
