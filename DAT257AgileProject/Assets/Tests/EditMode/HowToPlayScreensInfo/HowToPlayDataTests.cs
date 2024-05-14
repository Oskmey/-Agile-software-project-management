using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[TestFixture]
public class HowToPlayDataTests
{
    private static readonly HowToPlayData[] allHowToPlayData = Resources.LoadAll<HowToPlayData>("ScriptableObjects");

    public static IEnumerable<TestCaseData> HowToPlayDataTestCases
    {
        get
        {
            foreach (var howToPlayDataEntry in allHowToPlayData)
            {
                yield return new TestCaseData(howToPlayDataEntry);
            }
        }
    }

    [Test]
    public void AllHowToPlayData_IsNotNull()
    {
        Assert.IsNotNull(allHowToPlayData);
    }

    [Test]
    public void AllHowToPlayData_IsNotEmpty()
    {
        Assert.IsNotEmpty(allHowToPlayData);
    }

    [Test, TestCaseSource(nameof(HowToPlayDataTestCases))]
    public void EachHowToPlayData_HasScreenType(HowToPlayData howToPlayData)
    {
        Assert.IsNotNull(howToPlayData.ScreenType);
    }

    [Test, TestCaseSource(nameof(HowToPlayDataTestCases))]
    public void EachHowToPlayData_HasContentText(HowToPlayData howToPlayData)
    {
        Assert.IsNotNull(howToPlayData.ContentText);
    }

    [Test, TestCaseSource(nameof(HowToPlayDataTestCases))]
    public void EachHowToPlayData_HasNonEmptyContentText(HowToPlayData howToPlayData)
    {
        Assert.IsNotEmpty(howToPlayData.ContentText);
    }

    [Test, TestCaseSource(nameof(HowToPlayDataTestCases))]
    public void EachHowToPlayData_HasContentTextStartingWithCapitalLetter(HowToPlayData howToPlayData)
    {
        Assert.AreEqual(char.ToUpper(howToPlayData.ContentText[0]), howToPlayData.ContentText[0]);
    }

    [Test, TestCaseSource(nameof(HowToPlayDataTestCases))]
    public void EachHowToPlayData_DoesNotHaveContentTextEndingWithCarriageReturn(HowToPlayData howToPlayData)
    {
        Assert.AreNotEqual('\r', howToPlayData.ContentText[^1]);
    }

    [Test, TestCaseSource(nameof(HowToPlayDataTestCases))]
    public void EachHowToPlayData_DoesNotHaveContentTextEndingWithNewLine(HowToPlayData howToPlayData)
    {
        Assert.AreNotEqual('\n', howToPlayData.ContentText[^1]);
    }
}
