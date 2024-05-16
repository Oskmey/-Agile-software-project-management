using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            foreach (TrashRarity trashRarityEntry in GetTrashRarities())
            {
                yield return new TestCaseData(trashRarityEntry);
            }
        }
    }

    [Test, TestCaseSource(nameof(TrashRarityTestCases))]
    public void EachTrashRarity_IsCovered(TrashRarity trashRarityEntry)
    {
        Assert.AreEqual(System.Enum.GetValues(typeof(TrashRarity)).Length, TrashRarityTestCases.Count());
    }

    [Test, TestCaseSource(nameof(TrashRarityTestCases))]
    public void EachTrashRarity_HasReadableString(TrashRarity trashRarityEntry)
    {
        string trashRarityString = trashRarityEntry.ToReadableString();

        Assert.IsNotNull(trashRarityString);
        Assert.IsNotEmpty(trashRarityString);
    }

    [Test]
    public void TrashRarity_GetRandomRarity_ReturnsRarity()
    {
        TrashRarity trashRarity = TrashRarityExtensions.GetRandomRarity();

        Assert.IsNotNull(trashRarity);
        Assert.IsTrue(System.Enum.IsDefined(typeof(TrashRarity), trashRarity));
    }
}