using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TrashTypeTests
{
    private static List<TrashType> GetTrashTypes()
    {
        List<TrashType> trashTypes = new();
        foreach (TrashType trashType in System.Enum.GetValues(typeof(TrashType)))
        {
            trashTypes.Add(trashType);
        }

        return trashTypes;
    }
    private static IEnumerable<TestCaseData> TrashTypeTestCases
    {
        get
        {
            foreach (TrashType trashTypeEntry in GetTrashTypes())
            {
                yield return new TestCaseData(trashTypeEntry);
            }
        }
    }

    [Test, TestCaseSource(nameof(TrashTypeTestCases))]
    public void EachTrashType_IsCovered(TrashType trashTypeEntry)
    {
        // Check that all enum values are covered
        Assert.AreEqual(System.Enum.GetValues(typeof(TrashType)).Length, TrashTypeTestCases.Count());
    }

    [Test, TestCaseSource(nameof(TrashTypeTestCases))]
    public void EachTrashType_HasReadableString(TrashType trashTypeEntry)
    {
        string trashTypeString = trashTypeEntry.ToReadableString();
        Assert.IsNotNull(trashTypeString);
        Assert.IsNotEmpty(trashTypeString);
    }
}