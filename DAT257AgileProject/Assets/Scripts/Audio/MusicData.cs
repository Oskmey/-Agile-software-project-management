using UnityEngine;

[CreateAssetMenu(fileName = "NewMusicScriptableObject", menuName = "ScriptableObjects/MusicScriptableObject")]
public class MusicData : AudioData
{
    [SerializeField]
    private MusicName musicName;

    public MusicName MusicName => musicName;
}
