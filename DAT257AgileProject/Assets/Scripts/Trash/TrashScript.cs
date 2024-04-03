using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashScript : MonoBehaviour
{
    [SerializeField] private TrashData trashScriptableObject;

    // Attributes set via the scriptable object
    private TrashType trashType;
    private string trashInformation;
    private List<string> sourcesInformation;
    private int rarity;
    private int moneyValue;

    private void Start()
    {
        IntializeAttributesBasedOnScriptableObject();
    }

    private void Update()
    {
        
    }

    private void IntializeAttributesBasedOnScriptableObject()
    {
        trashType = trashScriptableObject.TrashType;
        trashInformation = trashScriptableObject.TrashInformation;
        sourcesInformation = new List<string>(trashScriptableObject.SourcesInformation);
        rarity = trashScriptableObject.Rarity;
        moneyValue = trashScriptableObject.MoneyValue;
    }

    public TrashType TrashType => trashType;
    public string TrashInformation => trashInformation;
    public IReadOnlyList<string> SourcesInformation => sourcesInformation;
    public int Rarity => rarity;
    public int MoneyValue => moneyValue;
}
