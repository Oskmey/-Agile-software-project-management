using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    // The fields need to be SerializeField or public.
    [SerializeField]
    private Vector2 playerPosition;
    public Vector2 PlayerPosition { get { return playerPosition; } set { playerPosition = value; } }

    [SerializeField]
    private int money;
    public int Money { get { return money; } set { money = value; } }

    [SerializeField]
    private int musicVolume;
    public int MusicVolume { get { return musicVolume; } set { musicVolume = value; } }

    [SerializeField]
    private int soundVolume;
    public int SoundVolume { get { return soundVolume; } set { soundVolume = value; } }

    [SerializeField]
    private SerializableDictionary<TrashType, int> recycledTrashCount;
    public SerializableDictionary<TrashType, int> RecycledTrashCount { get { return recycledTrashCount; } set { recycledTrashCount = value; } }

    [SerializeField]
    private List<InventoryItem> savedInventoryItems;
    public List<InventoryItem> SavedInventoryItems { get { return savedInventoryItems; } set { savedInventoryItems = value; } }

    // This field is relevant for multiple files and debugging.
    [SerializeField]
    private long lastUpdated;
    public long LastUpdated { get { return lastUpdated; } set { lastUpdated = value; } }

    public GameData()
    {
        // should maybe have that the "correct" starting point for the player be (0, 0)?
        playerPosition = new Vector2(-4, -2);
        money = 0;
        // TODO: Implement a way to change these. 
        // Should probably have a VolumeManager like how
        // The PlayerStatsManager works. 
        // Also chose to have them as integers because that is how it usually is
        // presented to the user. 
        musicVolume = 50;
        soundVolume = 50;
    }
}
