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
    public void EachTrashData_HasTrashRarity(TrashData trashDataEntry)
    {
        Assert.IsNotNull(trashDataEntry.Rarity);
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
