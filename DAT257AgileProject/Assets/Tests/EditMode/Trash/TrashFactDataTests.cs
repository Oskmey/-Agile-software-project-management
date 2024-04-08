using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[TestFixture]
public class TrashFactDataTests
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

    [Test, TestCaseSource(nameof(TrashFactTestCases))]
    public void EachTrashFact_HasAtLeastOneSource(TrashFactData trashFact)
    {
        Assert.IsNotEmpty(trashFact.SourcesInformation);
    }

    [Test, TestCaseSource(nameof(TrashFactTestCases))]
    public void EachTrashFact_EndsWithQuestionMark(TrashFactData trashFact)
    {
        Assert.IsTrue(trashFact.TrashFact.EndsWith("?"));
    }

    [Test, TestCaseSource(nameof(TrashFactTestCases))]
    public void EachTrashFact_StartsWithLowerCase(TrashFactData trashFact)
    {
        Assert.IsTrue(char.IsLower(trashFact.TrashFact[0]));
    }

    [Test, TestCaseSource(nameof(TrashFactTestCases))]
    public void EachTrashFact_HasAtLeastOneWord(TrashFactData trashFact)
    {
        Assert.IsTrue(trashFact.TrashFact.Contains(" "));
    }

    [Test, TestCaseSource(nameof(TrashFactTestCases))]
    public void EachTrashFact_HasAtLeastOneLetter(TrashFactData trashFact)
    {
        Assert.IsTrue(trashFact.TrashFact.Any(char.IsLetter));
    }    
}
