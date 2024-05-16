using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class AccessoryRarityTests
{
    private static List<AccessoryRarity> GetAccessoryRarities()
    {
        List<AccessoryRarity> accessoryRarities = new();
        foreach (AccessoryRarity accessoryRarity in System.Enum.GetValues(typeof(AccessoryRarity)))
        {
            accessoryRarities.Add(accessoryRarity);
        }

        return accessoryRarities;
    }
    private static IEnumerable<TestCaseData> AccessoryRarityTestCases
    {
        get
        {
            foreach (AccessoryRarity accessoryRarityEntry in GetAccessoryRarities())
            {
                yield return new TestCaseData(accessoryRarityEntry);
            }
        }
    }

    [Test, TestCaseSource(nameof(AccessoryRarityTestCases))]
    public void EachAccessoryRarity_IsCovered(AccessoryRarity accessoryRarityEntry)
    {
        Assert.AreEqual(System.Enum.GetValues(typeof(AccessoryRarity)).Length, AccessoryRarityTestCases.Count());
    }

    [Test, TestCaseSource(nameof(AccessoryRarityTestCases))]
    public void EachAccessoryRarity_HasReadableString(AccessoryRarity accessoryRarityEntry)
    {
        string accessoryRarityString = accessoryRarityEntry.ToReadableString();
        Assert.IsNotNull(accessoryRarityString);
        Assert.IsNotEmpty(accessoryRarityString);
    }
}
