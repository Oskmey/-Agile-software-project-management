using UnityEngine;

[CreateAssetMenu(fileName = "NewSoundScriptableObject", menuName = "ScriptableObjects/SoundScriptableObject")]
public class SoundData : AudioData
{
    [SerializeField]
    private SoundName soundName;

    public SoundName SoundName => soundName;
}
