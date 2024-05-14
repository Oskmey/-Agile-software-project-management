using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new AccessorySO", menuName = "ScriptableObjects/AccessoryScriptableObject")]
public class AccessorySO : ScriptableObject
{
    [Header("Accessory Settings")]
    [Tooltip("Name of the item")]
    [SerializeField]
    private string accessoryName;
    public string AccessoryName { get => accessoryName; }
    [Tooltip("Price of the item")]
    public int cost;
    [Tooltip("Sprite of the item")]
    public Sprite sprite;

    [Header("Accessory Rarity")]
    [Tooltip("Rarity of the item")]
    public AccessoryRarity rarity;

    [Header("Accessory effects")]
    [Tooltip("Effects the items has on the player")]
    [SerializeReference]
    public List<AEffect> accessoryEffects;
}

public enum AccessoryRarity
{
    Common = 50,
    Uncommon = 30,
    Rare = 15,
    Epic = 10,
    Legendary = 5
}

public static class AccessoryRarityExtensions
{
    public static string ToReadableString(this AccessoryRarity rarity)
    {
        switch (rarity)
        {
            case AccessoryRarity.Common:
                return "Common";
            case AccessoryRarity.Uncommon:
                return "Uncommon";
            case AccessoryRarity.Rare:
                return "Rare";
            case AccessoryRarity.Epic:
                return "Epic";
            case AccessoryRarity.Legendary:
                return "Legendary";
            default:
                return "Unknown";
        }
    }
}