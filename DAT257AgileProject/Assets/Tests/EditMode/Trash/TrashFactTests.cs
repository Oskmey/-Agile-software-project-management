using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TestFixture]
public class TrashFactTests
{
    private static readonly TrashFactData[] trashFacts = Resources.LoadAll<TrashFactData>("ScriptableObjects");

    public static IEnumerable<TestCaseData> TrashFactTestCases
    {
        get
        {
            foreach (var trashFact in trashFacts)
            {
                yield return new TestCaseData(trashFact);
            }
        }
    }

    [Test]
    public void TrashFacts_AreNotNull()
    {
        Assert.IsNotNull(trashFacts);
    }

    [Test]
    public void TrashFacts_AreNotEmpty()
    {
        Assert.IsNotEmpty(trashFacts);
    }

    [Test, TestCaseSource(nameof(TrashFactTestCases))]
    public void EachTrashFact_HasTrashFact(TrashFactData trashFact)
    {
        Assert.IsNotNull(trashFact.TrashFact);
    }

    [Test, TestCaseSource(nameof(TrashFactTestCases))]
    public void EachTrashFact_HasSourcesInformation(TrashFactData trashFact)
    {
        Assert.IsNotNull(trashFact.SourcesInformation);
    }
}
