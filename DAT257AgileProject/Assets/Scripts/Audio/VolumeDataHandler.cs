using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VolumeDataHandler : MonoBehaviour
{
    [Header("File Storage Configuration")]
    [SerializeField]
    private string fileName;

    private VolumeData volumeData;
    private List<IDataPersistence<VolumeData>> volumeDataPersistenceObjects;
    private FileDataHandler<VolumeData> volumeFileDataHandler;
    
    public VolumeDataHandler Instance { get; private set; }
    
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

        volumeDataPersistenceObjects = FindAllDataPersistenceObjects();
        volumeFileDataHandler = new FileDataHandler<VolumeData>(Application.persistentDataPath, fileName, false);
        LoadData();
    }

    public void LoadData()
    {
        volumeData = volumeFileDataHandler.Load();

        if (volumeData == null)
        {
            volumeData = new VolumeData();
            Debug.Log("New Volume Data created");
        }

        foreach (IDataPersistence<VolumeData> volumeDataPersistenceObject in volumeDataPersistenceObjects)
        {
            volumeDataPersistenceObject.LoadData(volumeData);
        }
    }

    public void SaveData()
    {
        if (volumeData == null)
        {
            Debug.LogWarning("No Volume Data found");
            return;
        }
        foreach (IDataPersistence<VolumeData> volumeDataPersistenceObject in volumeDataPersistenceObjects)
        {
            volumeDataPersistenceObject.SaveData(volumeData);
        }

        volumeFileDataHandler.Save(volumeData);
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

    private List<IDataPersistence<VolumeData>> FindAllDataPersistenceObjects()
    {
        // Can specify if should include inactive game objects.
        IEnumerable<IDataPersistence<VolumeData>> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence<VolumeData>>();
        return new List<IDataPersistence<VolumeData>>(dataPersistenceObjects);
    }
}
