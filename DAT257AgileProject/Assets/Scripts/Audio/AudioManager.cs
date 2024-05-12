using System;
using UnityEngine;

public class AudioManager : MonoBehaviour, IDataPersistence<SettingsData>
{
    public static AudioManager Instance { get; private set; }

    [SerializeField]
    private AudioSource musicSource;
    [SerializeField]
    private AudioSource soundSource;

    private MusicData[] musicSongs;
    private SoundData[] soundEffects;

    public float MasterVolume { get; private set; } 
    public float MusicVolume { get; private set; } 
    public float SoundVolume { get; private set; }

    // Default to one so that not affect sound when not changed value.
    private float lastMusicBalancingValue = 1;
    private float lastSoundBalancingValue = 1;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Found more than one Audio Manager in scene, destroying newest.");
            Destroy(this.gameObject);
            return;
        }
        musicSongs = Resources.LoadAll<MusicData>("ScriptableObjects");
        soundEffects = Resources.LoadAll<SoundData>("ScriptableObjects");

        Instance = this;

        // So that it stays between scenes. 
        DontDestroyOnLoad(this.gameObject);
    }

    public void PlayMusic(MusicName musicName)
    {
        MusicData selectedSong = Array.Find(musicSongs, song => song.MusicName == musicName);

        // Have to update the volume according to the balancing value in SO.
        lastMusicBalancingValue = selectedSong.AudioBalancingValue;
        UpdateMusicVolume(CalcMusicVolume());

        if (selectedSong != null)
        {
            musicSource.clip = selectedSong.AudioClip;
            musicSource.Play();
        }
        else
        {
            Debug.LogError("Selected Song was null");
        }
    }

    public void PlaySound(SoundName soundName)
    {
        SoundData selectedSound = Array.Find(soundEffects, sound => sound.SoundName == soundName);

        // Have to update the volume according to the balancing value in SO.
        lastSoundBalancingValue = selectedSound.AudioBalancingValue;
        UpdateSoundVolume(CalcSoundVolume());

        if (selectedSound != null)
        {
            AudioClip soundClip = selectedSound.AudioClip;
            soundSource.PlayOneShot(soundClip);
        }
        else
        {
            Debug.LogError("Selected Sound was null");
        }
    }

    private float CalcMusicVolume()
    {
        return MusicVolume * MasterVolume * lastMusicBalancingValue;
    }

    public void SetMusicVolume(float newVolume)
    {
        MusicVolume = newVolume;
        UpdateMusicVolume(CalcMusicVolume());
    }

    private void UpdateMusicVolume(float newVolume)
    {
        if (0 <= newVolume && newVolume <= 1)
        {
            musicSource.volume = newVolume;
        }
        else
        {
            Debug.LogWarning($"New Volume: {newVolume}, has to be in [0,1]");
        }
    }
    private float CalcSoundVolume()
    {
        return SoundVolume * MasterVolume * lastSoundBalancingValue;
    }

    public void SetSoundVolume(float newVolume)
    {
        SoundVolume = newVolume;
        UpdateSoundVolume(CalcSoundVolume());
    }

    private void UpdateSoundVolume(float newVolume)
    {
        if (0 <= newVolume && newVolume <= 1)
        {
            soundSource.volume = newVolume;
        }
        else
        {
            Debug.LogWarning($"New Volume: {newVolume}, has to be in [0,1]");
        }
    }

    public void SetMasterVolume(float newVolume)
    {
        MasterVolume = newVolume;
        UpdateMusicVolume(CalcMusicVolume());
        UpdateSoundVolume(CalcSoundVolume());
    }

    public void LoadData(SettingsData data)
    {
        MasterVolume = data.MasterVolume;
        MusicVolume = data.MusicVolume;
        SoundVolume = data.SoundVolume;
    }

    public void SaveData(SettingsData data)
    {
        data.MasterVolume = MasterVolume;
        data.MusicVolume = MusicVolume;
        data.SoundVolume = SoundVolume;
    }
}
