using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldAudioInitializer : MonoBehaviour
{
    private static readonly string[] worldNames = {"First World", "Second World"};

    private void Start()
    {
        string currentWorldName = SceneManager.GetActiveScene().name;
        if (currentWorldName == worldNames[0])
        {
            AudioManager.Instance.PlayMusic(MusicName.WorldTheme);
        }
        else if (currentWorldName == worldNames[1])
        {
            AudioManager.Instance.PlayMusic(MusicName.WorldTheme);
        }
        else
        {
            Debug.LogWarning("World Name not added to WorldAudioInitializer");
        }
    }
}
