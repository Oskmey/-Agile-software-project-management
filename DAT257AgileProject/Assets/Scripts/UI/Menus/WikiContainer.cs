using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows;
using System.IO;

public class WikiContainer : MonoBehaviour
{
    [SerializeField] 
    private GameObject wikiItemPrefab;
    [SerializeField]
    private GameObject subheaderPrefab;
    private SubheaderHandler subheaderHandler;
    private WikiHandler wikiHandler;

    private void Start()
    {
        string[] folderPaths = {
            "Assets/Resources/ScriptableObjects/TrashFacts/CarTire",
            "Assets/Resources/ScriptableObjects/TrashFacts/CigaretteButt",
            "Assets/Resources/ScriptableObjects/TrashFacts/ElectricScooter",
            "Assets/Resources/ScriptableObjects/TrashFacts/GlassBottle",
            "Assets/Resources/ScriptableObjects/TrashFacts/PETBottle",
            "Assets/Resources/ScriptableObjects/TrashFacts/Plastic",
            "Assets/Resources/ScriptableObjects/TrashFacts/PlasticSpoon",
            "Assets/Resources/ScriptableObjects/TrashFacts/TrashBag"
        };

        foreach (string folderPath in folderPaths)
        {
            string folderName = System.IO.Path.GetFileName(folderPath);
            GameObject go = Instantiate(subheaderPrefab);
            subheaderHandler = go.GetComponent<SubheaderHandler>();
            go.transform.SetParent(transform, false);
            string[] parts = Regex.Split(folderName, @"(?<!^)(?=[A-Z])");
            string splitFolderName = string.Join(" ", parts);
            subheaderHandler.SetHeaderText(splitFolderName);
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
            go.transform.SetParent(transform, false);
            go.transform.localScale = Vector3.one;
            wikiHandler.SetFactText(trash.TrashFact);
            wikiHandler.SetSourceText(trash.SourcesInformation);
        }
    }
}
