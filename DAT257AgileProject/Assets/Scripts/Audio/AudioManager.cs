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
}
