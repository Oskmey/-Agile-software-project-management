using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

// Data persistence classes are based on:
// https://www.youtube.com/playlist?list=PL3viUl9h9k7-tMGkSApPdu4hlUBagKial

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Configuration")]
    [SerializeField]
    private string fileName;
    [SerializeField]
    private bool useEncryption;

    private GameData gameData;
    private List<IDataPersistence<GameData>> dataPersistenceObjects;
    private FileDataHandler<GameData> dataHandler;
    
    public static DataPersistenceManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Found more than one Data Persistence Manager in scene, destroying newest.");
            Destroy(this.gameObject);
            return;
        }

        Instance = this;

        // So that it stays between scenes. 
        DontDestroyOnLoad(this.gameObject);

        dataHandler = new FileDataHandler<GameData>(Application.persistentDataPath, fileName, useEncryption);
    }

    private void OnEnable()
    {
        // Can't be done in start since the scene loads before it. 
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void NewGame()
    {
        gameData = new GameData();
    }

    public void LoadGame()
    {
        // Want to load saved data from a file using the data handler
        gameData = dataHandler.Load();

        if (gameData == null)
        {
            Debug.LogWarning("No data found. A new game has to be started before loading");
            return;
        }

        foreach (IDataPersistence<GameData> dataPersistenceObject in dataPersistenceObjects)
        {
            dataPersistenceObject.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        if (gameData == null)
        {
            Debug.LogWarning("No data found. A new game has to be started before data can be saved");
            return;
        }

        foreach (IDataPersistence<GameData> dataPersistenceObject in dataPersistenceObjects)
        {
            dataPersistenceObject.SaveData(gameData);
        }

        // Timestamp when last saved.
        gameData.LastUpdated = System.DateTime.Now.ToBinary();

        dataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence<GameData>> FindAllDataPersistenceObjects()
    {
        // Can specify if should include inactive game objects.
        IEnumerable<IDataPersistence<GameData>> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence<GameData>>();
        return new List<IDataPersistence<GameData>>(dataPersistenceObjects);
    }

    public bool HasGameData()
    {
        return gameData != null;
    }
}
