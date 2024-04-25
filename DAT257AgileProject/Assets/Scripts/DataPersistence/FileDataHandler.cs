using System;
using System.IO;
using UnityEngine;

public class FileDataHandler
{
    private readonly string dataDirPath;
    private readonly string dataFileName;

    private readonly bool useEncryption;
    private readonly string encryptionCodeWord = "kingblob";

    public FileDataHandler(string dataDirPath, string dataFileName, bool useEncryption)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
        this.useEncryption = useEncryption;
    }

    // To support multiple save files, this would take in a profileId.
    public GameData Load()
    {
        // if we add multiple save files this will then include then profileId. 
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad;
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                if (useEncryption)
                {
                    dataToLoad = EncryptDecrypt(dataToLoad);
                }

                // deserialize the data
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError($"Error occurred when trying to load file {fullPath} \n{e}");
            }
        }
        return loadedData;
    }

    // To support multiple save files, this would take in a profileId.
    public void Save(GameData data)
    {
        // if we add multiple save files this will then include then profileId. 
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        try
        {
            string directoryName = Path.GetDirectoryName(fullPath);
            Directory.CreateDirectory(directoryName);

            string dataToStore = JsonUtility.ToJson(data, true);

            // optionally encrypt data
            if (useEncryption)
            {
                dataToStore = EncryptDecrypt(dataToStore);
            }

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error occurred when trying to save file {fullPath} \n{e}");
        }
    }

    // Simple implementation of XOR encryption. 
    private string EncryptDecrypt(string data)
    {
        string modifiedData = "";
        for (int i = 0; i < data.Length; i++)
        {
            modifiedData += (char)(data[i] ^ encryptionCodeWord[i % encryptionCodeWord.Length]);
        }
        return modifiedData;
    }
}
