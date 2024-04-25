using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new AccessorySO", menuName = "ScriptableObjects/AccessoryScriptableObject")]
public class AccessorySO : ScriptableObject
{
    [Header("Accessory Settings")]
    [Tooltip("Name of the item")]
    public string accessoryName;
    [Tooltip("Price of the item")]
    public int cost;
    [Tooltip("Sprite of the item")]
    public Sprite sprite;

    [Header("Accessory Rarity")]
    [Tooltip("Rarity of the item")]
    public rarity rarity;


    [Header("Accessory effects")]
    [Tooltip("Effects the items has on the player")]
    [SerializeField]
    public List<AEffect> accessoryEffects;
}



    public enum rarity
    {
        Common = 50,
        Uncommon = 30,
        Rare = 15,
        Epic = 10,
        Legendary = 5
    }


