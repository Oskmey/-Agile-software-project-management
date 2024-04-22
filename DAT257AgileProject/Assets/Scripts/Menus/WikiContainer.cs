using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WikiContainer : MonoBehaviour
{
    [SerializeField] 
    private GameObject wikiItemPrefab;
    [SerializeField]
    private GameObject subheaderPrefab;
    private SubheaderHandler subheaderHandler;
    private WikiHandler wikiHandler;

    // Start is called before the first frame update
    void Start()
    {
        string[] folderPaths = AssetDatabase.GetSubFolders("Assets/Resources/ScriptableObjects/TrashFacts");
        foreach (string folderPath in folderPaths)
        {
            string folderName = System.IO.Path.GetFileName(folderPath);
            GameObject go = Instantiate(subheaderPrefab);
            subheaderHandler = go.GetComponent<SubheaderHandler>();
            go.transform.parent = transform;
            subheaderHandler.SetHeaderText(folderName);
            go.transform.localScale = Vector3.one;
            GenerateWikiItems(folderName);
        }
    }
    
    private void GenerateWikiItems(string subFolder)
    {
        TrashFactData[] trashData = Resources.LoadAll<TrashFactData>($"ScriptableObjects/TrashFacts/{subFolder}");
        foreach (TrashFactData trash in trashData)
        {
            GameObject go = Instantiate(wikiItemPrefab);
            wikiHandler = go.GetComponent<WikiHandler>();
            go.transform.parent = transform;
            go.transform.localScale = Vector3.one;
            wikiHandler.SetFactText(trash.TrashFact);
            wikiHandler.SetSourceText(trash.SourcesInformation);
        }
    }
}
