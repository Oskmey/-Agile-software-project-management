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
    private SerializableDictionary<TrashType, int> recycledTrashCount;
    public SerializableDictionary<TrashType, int> RecycledTrashCount { get { return recycledTrashCount; } set { recycledTrashCount = value; } }

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
        money = 0;
    }
}
