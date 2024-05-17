using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[TestFixture]
public class TrashDataTests
{
    private static readonly TrashData[] trashData = Resources.LoadAll<TrashData>("ScriptableObjects");

    public static IEnumerable<TestCaseData> TrashDataTestCases
    {
        get
        {
            foreach (var trashDataEntry in trashData)
            {
                yield return new TestCaseData(trashDataEntry);
            }
        }
    }

    /* private static IEnumerable<TestCaseData> NonRecycableTrashDataTestCases
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
    }*/

    private static IEnumerable<TestCaseData> RecyclableTrashDataTestCases
    {
        get
        {
            foreach (TrashData trashDataEntry in trashData)
            {
                if (trashDataEntry.IsRecyclable)
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

        // Assert
        Assert.IsFalse(!trashDataEntry.IsRecyclable);
    }
    */

    [Test]
    public void TrashData_AreNotNull()
    {
        Assert.IsNotNull(trashData);
    }

    [Test]
    public void TrashData_AreNotEmpty()
    {
        Assert.IsNotEmpty(trashData);
    }


    [Test, TestCaseSource(nameof(RecyclableTrashDataTestCases))]
    public void Recycling_GivenRecyclableTrash_ReturnsTrue(TrashData trashDataEntry)
    {
        // Assign

        // Act

        // Assert
        Assert.IsTrue(trashDataEntry.IsRecyclable);
    }

    [Test, TestCaseSource(nameof(TrashDataTestCases))]
    public void EachTrashData_HasTrashCategories(TrashData trashDataEntry)
    {
        Assert.IsNotNull(trashDataEntry.TrashCategories);
    }

    [Test, TestCaseSource(nameof(TrashDataTestCases))]
    public void EachTrashData_HasAtLeastOneTrashCategory(TrashData trashDataEntry)
    {
        Assert.IsNotEmpty(trashDataEntry.TrashCategories);
    }

    [Test, TestCaseSource(nameof(TrashDataTestCases))]
    public void EachTrashData_HasTrashFacts(TrashData trashDataEntry)
    {
        Assert.IsNotNull(trashDataEntry.TrashFacts);
    }

    [Test, TestCaseSource(nameof(TrashDataTestCases))]
    public void EachTrashData_HasAtLeastOneTrashFact(TrashData trashDataEntry)
    {
        Assert.IsNotEmpty(trashDataEntry.TrashFacts);
    }

    [Test, TestCaseSource(nameof(TrashDataTestCases))]
    public void EachTrashData_HasMoneyValue(TrashData trashDataEntry)
    {
        Assert.IsNotNull(trashDataEntry.MoneyValue);
    }

    [Test, TestCaseSource(nameof(TrashDataTestCases))]
    public void EachTrashData_HasMoneyValueGreaterThanZero(TrashData trashDataEntry)
    {
        Assert.Greater(trashDataEntry.MoneyValue, 0);
    }

    [Test, TestCaseSource(nameof(TrashDataTestCases))]
    public void EachTrashData_HasIsRecyclable(TrashData trashDataEntry)
    {
        Assert.IsNotNull(trashDataEntry.IsRecyclable);
    }
}
