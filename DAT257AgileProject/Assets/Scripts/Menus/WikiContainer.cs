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

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
