using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashScript : MonoBehaviour
{
    [SerializeField] 
    private TrashData trashScriptableObject;

    // Attributes set via the scriptable object
    private TrashType trashType;
    private string trashInformation;
    private List<SourceData> sourcesInformation;
    private int rarity;
    private int moneyValue;

    // Awake and not Start for it to set the attributes
    // when the object is created
    private void Awake()
    {
        IntializeAttributesBasedOnScriptableObject();
    }

    private void IntializeAttributesBasedOnScriptableObject()
    {
        trashType = trashScriptableObject.TrashType;
        trashInformation = trashScriptableObject.TrashInformation;
        sourcesInformation = new List<SourceData>(trashScriptableObject.SourcesInformation);
        rarity = trashScriptableObject.Rarity;
        moneyValue = trashScriptableObject.MoneyValue;
    }

    public TrashType TrashType => trashType;
    public string TrashInformation => trashInformation;
    public IReadOnlyList<SourceData> SourcesInformation => sourcesInformation;
    public int Rarity => rarity;
    public int MoneyValue => moneyValue;
}
