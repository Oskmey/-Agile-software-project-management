using Inventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameData
{
    // The fields need to be SerializeField or public.
    [SerializeField]
    private SerializableDictionary<string, Vector2> playerPosition;
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

    [SerializeField]
    private string currentLevel;
    public string CurrentLevel { get { return currentLevel; } set { currentLevel = value; } }

    public GameData()
    {
        currentMoney = 0;
        totalMoneyEarned = 0;
        totalMoneySpent = 0;
        currentLevel = "First World";
    }

    public Vector2 GetPlayerPosition(string sceneName)
    {
        if (playerPosition.ContainsKey(sceneName))
        {
            return playerPosition[sceneName];
        }
        Debug.LogWarning("No player position found for scene: " + sceneName);
        return Vector2.zero;
    }

    public void SetPlayerPosition(string sceneName, Vector2 position)
    {
        if(playerPosition.ContainsKey(sceneName))
        {
            playerPosition[sceneName] = position;
            return;
        }

        int sceneCount = SceneManager.sceneCountInBuildSettings;     

        for( int i = 0; i < sceneCount; i++ )
        {
            if(sceneName == System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i)))
            {
                playerPosition.Add(sceneName, position);
                return;
            }
            else
            {
                Debug.LogWarning("Scene not found in build settings: " + sceneName);
            }
        }
    }
}
