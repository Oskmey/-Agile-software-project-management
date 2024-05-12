using Inventory.Model;
using System;
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
    private int currentMoney;
    public int CurrentMoney { get { return currentMoney; } set { currentMoney = value; } }

    [SerializeField]
    private int totalMoneyEarned;
    public int TotalMoneyEarned { get { return totalMoneyEarned; } set { totalMoneyEarned = value; } }
    
    [SerializeField]
    private int totalMoneySpent;
    public int TotalMoneySpent { get { return totalMoneySpent; } set { totalMoneySpent = value; } }
    
    [SerializeField]
    private SerializableDictionary<AccessorySO, int> purchasedAccessories;
    public SerializableDictionary<AccessorySO, int> PurchasedAccessories { get { return purchasedAccessories; } set { purchasedAccessories = value; } }

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
    private SerializableDictionary<TrashType, int> trashCaught;
    public SerializableDictionary<TrashType, int> TrashCaught { get { return trashCaught; } set { trashCaught = value; } }

    [SerializeField]
    private List<InventoryItem> savedInventoryItems;
    public List<InventoryItem> SavedInventoryItems { get { return savedInventoryItems; } set { savedInventoryItems = value; } }

    [SerializeField]
    private List<InventoryItem> savedAccessoryItems;
    public List<InventoryItem> SavedAccessoryItems { get { return savedAccessoryItems; } set { savedAccessoryItems = value; } }

    // This field is relevant for multiple files and debugging.
    [SerializeField]
    private long lastUpdated;
    public long LastUpdated { get { return lastUpdated; } set { lastUpdated = value; } }

    public GameData()
    {
        // should maybe have that the "correct" starting point for the player be (0, 0)?
        playerPosition = new Vector2(-4, -2);
        currentMoney = 0;
        totalMoneyEarned = 0;
        totalMoneySpent = 0;
        // TODO: Implement a way to change these. 
        // Should probably have a VolumeManager like how
        // The PlayerStatsManager works. 
        // Also chose to have them as integers because that is how it usually is
        // presented to the user. 
        musicVolume = 50;
        soundVolume = 50;
    }
}
