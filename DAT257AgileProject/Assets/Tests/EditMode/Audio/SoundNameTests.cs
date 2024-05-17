using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SoundNameTests
{
    private static List<SoundName> GetSoundNames()
    {
        List<SoundName> soundNames = new();
        foreach (SoundName soundName in System.Enum.GetValues(typeof(SoundName)))
        {
            soundNames.Add(soundName);
        }

        return soundNames;
    }
    private static IEnumerable<TestCaseData> SoundNameTestCases
    {
        get
        {
            foreach (SoundName soundNameEntry in GetSoundNames())
            {
                yield return new TestCaseData(soundNameEntry);
            }
        }
    }

    [Test, TestCaseSource(nameof(SoundNameTestCases))]
    public void EachSoundName_IsCovered(SoundName soundNameEntry)
    {
        // Check that all enum values are covered
        Assert.AreEqual(System.Enum.GetValues(typeof(SoundName)).Length, SoundNameTestCases.Count());
    }

    [Test, TestCaseSource(nameof(SoundNameTestCases))]
    public void EachSoundName_HasReadableString_NotNull(SoundName soundNameEntry)
    {
        string soundNameString = soundNameEntry.ToReadableString();
        Assert.IsNotNull(soundNameString);
    }

    [Test, TestCaseSource(nameof(SoundNameTestCases))]
    public void EachSoundName_HasReadableString_NotEmpty(SoundName soundNameEntry)
    {
        string soundNameString = soundNameEntry.ToReadableString();
        Assert.IsNotEmpty(soundNameString);
    }
}
