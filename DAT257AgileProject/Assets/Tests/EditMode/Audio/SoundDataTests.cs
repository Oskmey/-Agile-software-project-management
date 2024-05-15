using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

// For the functionality that is shared between SoundData and MusicData
// can be found in AudioData, so look in AudioDataTests for those tests.
[TestFixture]
public class SoundDataTests
{
    private static readonly SoundData[] allSoundData = Resources.LoadAll<SoundData>("ScriptableObjects");

    public static IEnumerable<TestCaseData> SoundDataTestCases
    {
        get
        {
            foreach (var soundDataEntry in allSoundData)
            {
                yield return new TestCaseData(soundDataEntry);
            }
        }
    }

    [Test]
    public void AllSoundData_IsNotNull()
    {
        Assert.IsNotNull(allSoundData);
    }

    [Test]
    public void AllSoundData_IsNotEmpty()
    {
        Assert.IsNotEmpty(allSoundData);
    }

    [Test, TestCaseSource(nameof(SoundDataTestCases))]
    public void EachSoundData_HasSoundName(SoundData soundData)
    {
        Assert.IsNotNull(soundData.SoundName);
    }
} 
