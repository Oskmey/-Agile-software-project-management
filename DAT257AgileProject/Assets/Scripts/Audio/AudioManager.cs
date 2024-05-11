using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
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

    private void Awake()
    {
        // TODO: load this from save.
        MasterVolume = 1;
        MusicVolume = 1;
        SoundVolume = 1;

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

    public void SetMusicVolume(float newVolume)
    {
        MusicVolume = newVolume;
        UpdateMusicVolume(MusicVolume * MasterVolume);
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

    public void SetSoundVolume(float newVolume)
    {
        SoundVolume = newVolume;
        UpdateSoundVolume(SoundVolume * MasterVolume);
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
        UpdateMusicVolume(MusicVolume * MasterVolume);
        UpdateSoundVolume(SoundVolume * MasterVolume);
    }
}
