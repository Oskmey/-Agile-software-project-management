using System.Collections;
using System.Collections.Generic;
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
            foreach (var trashTypeEntry in GetTrashTypes())
            {
                yield return new TestCaseData(trashTypeEntry);
            }
        }
    }

    [Test, TestCaseSource(nameof(TrashTypeTestCases))]
    public void EachTrashType_HasReadableString(TrashType trashTypeEntry)
    {
        string trashTypeString = trashTypeEntry.ToReadableString();
        Assert.IsNotNull(trashTypeString);
    }
}