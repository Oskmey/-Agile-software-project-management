using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[TestFixture]
public class AudioDataTests
{
    private static readonly AudioData[] allAudioData = Resources.LoadAll<AudioData>("ScriptableObjects");

    public static IEnumerable<TestCaseData> AudioDataTestCases
    {
        get
        {
            foreach (var audioDataEntry in allAudioData)
            {
                yield return new TestCaseData(audioDataEntry);
            }
        }
    }

    [Test]
    public void AllAudioData_IsNotNull()
    {
        Assert.IsNotNull(allAudioData);
    }

    [Test]
    public void AllAudioData_IsNotEmpty()
    {
        Assert.IsNotEmpty(allAudioData);
    }

    [Test, TestCaseSource(nameof(AudioDataTestCases))]
    public void EachAudioData_HasAudioClip(AudioData audioData)
    {
        Assert.IsNotNull(audioData.AudioClip);
    }

    [Test, TestCaseSource(nameof(AudioDataTestCases))]
    public void EachAudioData_HasAudioBalancingValue(AudioData audioData)
    {
        Assert.IsNotNull(audioData.AudioBalancingValue);
    }

    [Test, TestCaseSource(nameof(AudioDataTestCases))]
    public void EachAudioData_HasNotAudioBalancingValueGreaterThanOne(AudioData audioData)
    {
        Assert.LessOrEqual(audioData.AudioBalancingValue, 1);
    }

    [Test, TestCaseSource(nameof(AudioDataTestCases))]
    public void EachAudioData_HasAudioBalancingValueGreaterThanZero(AudioData audioData)
    {
        Assert.Greater(audioData.AudioBalancingValue, 0);
    }
}
