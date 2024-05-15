using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TrashRarityTests
{
    private static List<TrashRarity> GetTrashRarities()
    {
        List<TrashRarity> trashRarities = new();
        foreach (TrashRarity trashRarity in System.Enum.GetValues(typeof(TrashRarity)))
        {
            trashRarities.Add(trashRarity);
        }

        return trashRarities;
    }
    private static IEnumerable<TestCaseData> TrashRarityTestCases
    {
        get
        {
            foreach (var trashRarityEntry in GetTrashRarities())
            {
                yield return new TestCaseData(trashRarityEntry);
            }
        }
    }

    [Test, TestCaseSource(nameof(TrashRarityTestCases))]
    public void EachTrashRarity_HasReadableString(TrashRarity trashRarityEntry)
    {
        string trashRarityString = trashRarityEntry.ToReadableString();
        Assert.IsNotNull(trashRarityString);
    }

    [Test]
    public void TrashRarity_GetRandomRarity_ReturnsRarity()
    {
        TrashRarity trashRarity = TrashRarityExtensions.GetRandomRarity();
        Assert.IsNotNull(trashRarity);
        Assert.IsTrue(System.Enum.IsDefined(typeof(TrashRarity), trashRarity));
    }
}