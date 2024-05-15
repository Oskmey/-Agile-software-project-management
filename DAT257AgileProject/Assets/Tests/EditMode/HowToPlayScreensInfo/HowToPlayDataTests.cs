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

    // for the tests that check for invalid characters
    public static IEnumerable<TestCaseData> HowToPlayDataInvalidCharTestCases
    {
        get
        {
            char[] invalidChars = new[] { '\r', '\n' };
            foreach (var howToPlayDataEntry in allHowToPlayData)
            {
                foreach (var invalidChar in invalidChars)
                {
                    yield return new TestCaseData(invalidChar, howToPlayDataEntry);
                }
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

    [Test, TestCaseSource(nameof(HowToPlayDataInvalidCharTestCases))]
    public void EachHowToPlayData_DoesNotHaveContentTextEndingWithInvalidCharacter(char invalidChar, HowToPlayData howToPlayData)
    {
        Assert.AreNotEqual(invalidChar, howToPlayData.ContentText[^1]);
    }
}
