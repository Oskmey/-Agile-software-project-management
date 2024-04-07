using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashScript : MonoBehaviour
{
    [SerializeField] 
    private TrashData trashScriptableObject;

    // Attributes set via the scriptable object
    private TrashType trashType;
    private List<TrashFactData> sourcesInformation;
    private TrashRarity rarity;
    private int moneyValue;

    // Awake and not Start for it to set the attributes
    // when the object is created
    private void Awake()
    {
        LoadAttributesFromScriptableObject();
    }

    private void LoadAttributesFromScriptableObject()
    {
        trashType = trashScriptableObject.TrashType;
        sourcesInformation = new List<TrashFactData>(trashScriptableObject.SourcesInformation);
        rarity = trashScriptableObject.Rarity;
        moneyValue = trashScriptableObject.MoneyValue;
    }

    public TrashType TrashType => trashType;
    public IReadOnlyList<TrashFactData> SourcesInformation => sourcesInformation;
    public TrashRarity Rarity => rarity;
    public int MoneyValue => moneyValue;
}
