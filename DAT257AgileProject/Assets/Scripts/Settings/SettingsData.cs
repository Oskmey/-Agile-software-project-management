using UnityEngine;

[System.Serializable]
public class SettingsData
{
    [SerializeField]
    private float masterVolume;
    public float MasterVolume { get { return masterVolume; } set { masterVolume = value; } }
    [SerializeField]
    private float musicVolume;
    public float MusicVolume { get { return musicVolume; } set { musicVolume = value; } }
    [SerializeField]
    private float soundVolume;
    public float SoundVolume { get { return soundVolume; } set { soundVolume = value; } }

    public SettingsData()
    {
        masterVolume = 1;
        musicVolume = 0.5f;
        soundVolume = 0.5f;
    }
}
