using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TrashCategoryTests
{
    private static List<TrashCategory> GetTrashCategories()
    {
        List<TrashCategory> trashCategories = new();
        foreach (TrashCategory trashCategory in System.Enum.GetValues(typeof(TrashCategory)))
        {
            trashCategories.Add(trashCategory);
        }

        return trashCategories;
    }
    private static IEnumerable<TestCaseData> TrashCategoryTestCases
    {
        get
        {
            foreach (var trashCategoryEntry in GetTrashCategories())
            {
                yield return new TestCaseData(trashCategoryEntry);
            }
        }
    }

    [Test, TestCaseSource(nameof(TrashCategoryTestCases))]
    public void EachTrashCategory_HasReadableString(TrashCategory trashCategoryEntry)
    {
        string trashCategoryString = trashCategoryEntry.ToReadableString();
        Assert.IsNotNull(trashCategoryString);
    }
}
