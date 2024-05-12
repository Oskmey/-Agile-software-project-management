using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsDataHandler : MonoBehaviour
{
    [Header("File Storage Configuration")]
    [SerializeField]
    private string fileName;

    private SettingsData settingsData;
    private List<IDataPersistence<SettingsData>> settingsDataPersistenceObjects;
    private FileDataHandler<SettingsData> settingsFileDataHandler;
    
    public SettingsDataHandler Instance { get; private set; }
    
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

        settingsDataPersistenceObjects = FindAllDataPersistenceObjects();
        settingsFileDataHandler = new FileDataHandler<SettingsData>(Application.persistentDataPath, fileName, false);
        LoadData();
    }

    public void LoadData()
    {
        settingsData = settingsFileDataHandler.Load();

        if (settingsData == null)
        {
            settingsData = new SettingsData();
            Debug.Log("New Settings Data created");
        }

        foreach (IDataPersistence<SettingsData> settingsDataPersistenceObject in settingsDataPersistenceObjects)
        {
            settingsDataPersistenceObject.LoadData(settingsData);
        }
    }

    public void SaveData()
    {
        if (settingsData == null)
        {
            Debug.LogWarning("No Settings Data found");
            return;
        }
        foreach (IDataPersistence<SettingsData> settingsDataPersistenceObject in settingsDataPersistenceObjects)
        {
            settingsDataPersistenceObject.SaveData(settingsData);
        }

        settingsFileDataHandler.Save(settingsData);
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

    private List<IDataPersistence<SettingsData>> FindAllDataPersistenceObjects()
    {
        // Can specify if should include inactive game objects.
        IEnumerable<IDataPersistence<SettingsData>> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence<SettingsData>>();
        return new List<IDataPersistence<SettingsData>>(dataPersistenceObjects);
    }
}
