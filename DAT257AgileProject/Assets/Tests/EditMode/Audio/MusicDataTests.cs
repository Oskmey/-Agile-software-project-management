using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

// For the functionality that is shared between SoundData and MusicData
// can be found in AudioData, so look in AudioDataTests for those tests.
[TestFixture]
public class MusicDataTests
{
    private static readonly MusicData[] allMusicData = Resources.LoadAll<MusicData>("ScriptableObjects");
    public static IEnumerable<TestCaseData> MusicDataTestCases
    {
        get
        {
            foreach (var musicDataEntry in allMusicData)
            {
                yield return new TestCaseData(musicDataEntry);
            }
        }
    }

    [Test]
    public void AllMusicData_IsNotNull()
    {
        Assert.IsNotNull(allMusicData);
    }

    [Test]
    public void AllMusicData_IsNotEmpty()
    {
        Assert.IsNotEmpty(allMusicData);
    }

    [Test, TestCaseSource(nameof(MusicDataTestCases))]
    public void EachMusicData_HasMusicName(MusicData musicData)
    {
        Assert.IsNotNull(musicData.MusicName);
    }

}
