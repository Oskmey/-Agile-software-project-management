using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class RecyclingTests
{
    private RecyclingMachine recyclingMachine;
    private static readonly TrashData[] trashData = Resources.LoadAll<TrashData>("ScriptableObjects");

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

    private static IEnumerable<TestCaseData> NonRecycableTrashDataTestCases
    {
        get
        {
            foreach (TrashData trashDataEntry in trashData)
            {
                if (!trashDataEntry.IsRecyclable)
                {
                    yield return new TestCaseData(trashDataEntry);
                }
            }
        }
    }

    private static IEnumerable<TestCaseData> RecycableTrashDataTestCases
    {
        get
        {
            foreach (TrashData trashDataEntry in trashData)
            {
                if(trashDataEntry.IsRecyclable)
                {
                    yield return new TestCaseData(trashDataEntry);
                }
            }
        }
    }

    // Test for if we are adding non recycable trash data
    /*
    [Test, TestCaseSource(nameof(NonRecycableTrashDataTestCases))]
    public void Recycling_GivenNonRecylcableTrash_ReturnsFalse(TrashData trashDataEntry)
    {
        // Assign
        // Act
        bool isRecycable = recyclingMachine.IsTrashRecyclable(trashDataEntry);

        // Assert
        Assert.IsFalse(isRecycable);
    }
    */

    [Test, TestCaseSource(nameof(RecycableTrashDataTestCases))]
    public void Recycling_GivenRecyclableTrash_ReturnsTrue(TrashData trashDataEntry)
    {
        // Assign

        // Act
        bool isRecycable = recyclingMachine.IsTrashRecyclable(trashDataEntry);

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
