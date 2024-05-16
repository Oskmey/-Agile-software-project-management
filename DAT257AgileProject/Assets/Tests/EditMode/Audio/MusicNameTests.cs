using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MusicNameTests
{
    private static List<MusicName> GetMusicNames()
    {
        List<MusicName> musicNames = new();
        foreach (MusicName musicName in System.Enum.GetValues(typeof(MusicName)))
        {
            musicNames.Add(musicName);
        }

        return musicNames;
    }
    private static IEnumerable<TestCaseData> MusicNameTestCases
    {
        get
        {
            foreach (MusicName musicNameEntry in GetMusicNames())
            {
                yield return new TestCaseData(musicNameEntry);
            }
        }
    }

    [Test, TestCaseSource(nameof(MusicNameTestCases))]
    public void EachMusicName_IsCovered(MusicName musicNameEntry)
    {
        // Check that all enum values are covered
        Assert.AreEqual(System.Enum.GetValues(typeof(MusicName)).Length, MusicNameTestCases.Count());
    }

    [Test, TestCaseSource(nameof(MusicNameTestCases))]
    public void EachMusicName_HasReadableString(MusicName musicNameEntry)
    {
        string musicNameString = musicNameEntry.ToReadableString();
        Assert.IsNotNull(musicNameString);
        Assert.IsNotEmpty(musicNameString);
    }
}